using Microsoft.Extensions.DependencyInjection;

namespace MitMediator.AutoApi.HttpMediator;

public class HttpMediator : IClientMediator
{
    private readonly IServiceProvider _serviceProvider;
    
    private readonly string? _baseUrl;

    private readonly string? _httpClientName;

    public HttpMediator(IServiceProvider serviceProvider, string? baseUrl, string? httpClientName = null)
    {
        _serviceProvider = serviceProvider;
        _baseUrl = baseUrl;
        _httpClientName = httpClientName;
    }
    
    public ValueTask<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>
    {
        var behaviors = _serviceProvider
            .GetServices<IClientPipelineBehavior<TRequest, TResponse>>();
        
        var handler = new HttpRequestHandler<TRequest, TResponse>(_serviceProvider, _baseUrl, _httpClientName);
        
        using var behaviorEnumerator = behaviors.GetEnumerator();
        var pipeline = new ClientRequestPipeline<TRequest, TResponse>(behaviorEnumerator, handler);
        return pipeline.InvokeAsync(request, cancellationToken);
    }

    public ValueTask<Unit> SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest
    {
        return SendAsync<TRequest, Unit>(request, cancellationToken);
    }
    
}