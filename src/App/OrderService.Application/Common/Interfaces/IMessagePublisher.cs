namespace OrderService.Application.Common.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message);
}