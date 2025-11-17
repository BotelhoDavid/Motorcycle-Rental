using RabbitMQ.Client;

namespace Rent.Infra.CrossCutting.RabbitMQ.Producers
{
    public interface IRabbitMqContext
    {
        public Task<IConnection> CreateConnectionAsync();
        string QueueName { get; }
        string ExchangeName { get; }
    }
}
