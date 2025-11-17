namespace Rent.Domain.Interfaces.MessageBus
{
    public interface IRabbitMqProducer
    {
        Task PublishAsync<T>(T message, string? routingKey = null);
    }
}
