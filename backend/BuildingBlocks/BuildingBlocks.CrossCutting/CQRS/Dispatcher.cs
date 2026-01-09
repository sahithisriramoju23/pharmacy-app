using Microsoft.Extensions.DependencyInjection;
namespace BuildingBlocks.CrossCutting.CQRS;

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken cancellationToken = default);
public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) 
        where TRequest : ICommand<TResponse>
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TRequest, TResponse>>();
        RequestHandlerDelegate<TResponse> handlerDelegate = ct => handler.Handle(request, ct);
        
        return await handlerDelegate(cancellationToken);
    }
}
