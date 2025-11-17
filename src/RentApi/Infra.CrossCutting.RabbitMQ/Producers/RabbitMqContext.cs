using RabbitMQ.Client;
using System.Collections;

namespace Rent.Infra.CrossCutting.RabbitMQ.Producers
{
    public class RabbitMqContext : IRabbitMqContext
    {
        public string QueueName { get; private set; } = string.Empty;
        public string ExchangeName { get; private set; } = string.Empty;

        public async Task<IConnection> CreateConnectionAsync()
        {
            IDictionary vars = Environment.GetEnvironmentVariables();

            var factory = new ConnectionFactory
            {
                HostName = vars["RABBITMQ_HOST"]?.ToString() ?? "rabbitmq",
                Port = int.Parse(vars["RABBITMQ_PORT"]?.ToString() ?? "5672"),
                UserName = vars["RABBITMQ_DEFAULT_USER"]?.ToString() ?? "guest",
                Password = vars["RABBITMQ_DEFAULT_PASS"]?.ToString() ?? "guest",
                VirtualHost = vars["RABBITMQ_VHOST"]?.ToString() ?? "/",
            };

            QueueName = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE") ?? string.Empty;
            ExchangeName = Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE") ?? string.Empty;

            return await factory.CreateConnectionAsync();
        }
    }
}
