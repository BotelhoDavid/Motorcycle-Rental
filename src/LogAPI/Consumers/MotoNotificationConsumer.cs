using LogAPI.Entities;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections;
using System.Text;
using System.Text.Json;

namespace LogAPI
{
    public class MotoNotificationConsumer : BackgroundService
    {
        private readonly IMongoCollection<LogEntry> _logCollection;
        private readonly ConnectionFactory _factory;

        public string QueueName { get; private set; } = string.Empty;
        public string ExchangeName { get; private set; } = string.Empty;
        public string RoutingKey { get; private set; } = string.Empty;
        public string MongoCollection { get; private set; } = string.Empty;
        public string MongoDatabase { get; private set; } = string.Empty;

        public MotoNotificationConsumer(IMongoClient mongoClient)
        {
            IDictionary vars = Environment.GetEnvironmentVariables();

            MongoCollection = vars["MONGO_COLLECTION"]?.ToString() ?? "Logs";
            MongoDatabase = vars["MONGO_DATABASE"]?.ToString() ?? "Moto_Rental_Logs";

            var database = mongoClient.GetDatabase(MongoDatabase);
            _logCollection = database.GetCollection<LogEntry>(MongoCollection);

            // Corrigido: agora atribui à variável _factory (antes criava variável local)
            _factory = new ConnectionFactory
            {
                HostName = vars["RABBITMQ_HOST"]?.ToString() ?? "localhost",
                Port = int.Parse(vars["RABBITMQ_PORT"]?.ToString() ?? "5672"),
                UserName = vars["RABBITMQ_DEFAULT_USER"]?.ToString() ?? "guest",
                Password = vars["RABBITMQ_DEFAULT_PASS"]?.ToString() ?? "guest",
                ConsumerDispatchConcurrency = 1
            };

            QueueName = vars["RABBITMQ_QUEUE"]?.ToString() ?? "motos.notification";
            ExchangeName = vars["RABBITMQ_EXCHANGE"]?.ToString() ?? "motos.exchange";
            RoutingKey = vars["RABBITMQ_ROUTING_KEY"]?.ToString() ?? "motos.notification";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            // Declara queue
            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                try
                {
                    // Filtra pela routing key desejada
                    if (ea.RoutingKey != RoutingKey)
                    {
                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                        return;
                    }

                    var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                    var logEntry = JsonSerializer.Deserialize<LogEntry>(json);

                    if (logEntry != null)
                        await _logCollection.InsertOneAsync(logEntry, cancellationToken: stoppingToken);

                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                }
                catch (Exception err)
                {
                    await channel.BasicNackAsync(ea.DeliveryTag, false, requeue: true);
                }
            };

            await channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: false,
                consumer: consumer
            );

            // Mantém o consumer vivo
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
