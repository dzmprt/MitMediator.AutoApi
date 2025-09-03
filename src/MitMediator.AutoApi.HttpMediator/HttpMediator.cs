using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.HttpMediator.Extensions;

namespace MitMediator.AutoApi.HttpMediator;

public class HttpMediator : IClientMediator
{
    private readonly IServiceProvider _serviceProvider;

    private readonly string? _baseUrl;

    private readonly IHttpClientFactory _httpClientFactory;

    private readonly string? _httpClientName;

    public HttpMediator(
        IServiceProvider serviceProvider,
        string? baseUrl,
        string? httpClientName = null)
    {
        _serviceProvider = serviceProvider;
        _baseUrl = baseUrl;
        _httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
        _httpClientName = httpClientName;

    }

    public ValueTask<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
    {
        var behaviors = _serviceProvider
            .GetServices<IClientPipelineBehavior<TRequest, TResponse>>();

        var handler = new HttpRequestHandler<TRequest, TResponse>(_serviceProvider, _baseUrl, CreateHttpClient());

        using var behaviorEnumerator = behaviors.GetEnumerator();
        var pipeline = new ClientRequestPipeline<TRequest, TResponse>(behaviorEnumerator, handler);
        return pipeline.InvokeAsync(request, cancellationToken);
    }

    public ValueTask<Unit> SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        return SendAsync<TRequest, Unit>(request, cancellationToken);
    }

    public string GetRequestAbsoluteUrl<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
    {
        return $"{CreateHttpClient().BaseAddress}{HttpRequestsHelper.GetUrl(request, _baseUrl)}";
    }

    public string GetRequestAbsoluteUrl<TRequest>(TRequest request) where TRequest : IRequest
    {
        return $"{CreateHttpClient().BaseAddress}{HttpRequestsHelper.GetUrl(request, _baseUrl)}";
    }

    private HttpClient CreateHttpClient()
    {
        return _httpClientName is null
            ? _httpClientFactory.CreateClient()
            : _httpClientFactory.CreateClient(_httpClientName);
    }
}