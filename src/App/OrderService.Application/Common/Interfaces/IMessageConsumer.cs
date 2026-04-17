using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Application.Common.Interfaces;

public interface IMessageConsumer
{
    public void Consume<T>(
        string queueName, 
        Func<T, IServiceProvider, Task> handler, 
        IServiceScopeFactory scopeFactory);
}