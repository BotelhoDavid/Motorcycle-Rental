using RabbitMQ.Client;
using Rent.Domain.Interfaces.MessageBus;
using System.Text;
using System.Text.Json;

namespace Rent.Infra.CrossCutting.RabbitMQ.Producers
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        private readonly IRabbitMqContext _context;

        public RabbitMqProducer(IRabbitMqContext context)
        {
            _context = context;
        }

        public async Task PublishAsync<T>(T message, string? routingKey = null)
        {
            routingKey ??= _context.QueueName;

            using var connection = await _context.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: _context.ExchangeName,
                type: "direct",
                durable: true,
                autoDelete: false
            );

            await channel.QueueDeclareAsync(
                queue: _context.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            await channel.QueueBindAsync(
                queue: _context.QueueName,
                exchange: _context.ExchangeName,
                routingKey: routingKey
            );

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            var props = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent
            };

            await channel.BasicPublishAsync(
                exchange: _context.ExchangeName,
                routingKey: routingKey,
                mandatory: false,
                basicProperties: props,
                body: body
            );
        }
    }
}