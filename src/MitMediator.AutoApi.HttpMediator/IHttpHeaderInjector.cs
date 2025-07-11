namespace MitMediator.AutoApi.HttpMediator;

/// <summary>
/// Http header injector for requests.
/// </summary>
/// <typeparam name="TRequest"><see cref="TRequest"/></typeparam>
/// <typeparam name="TResponse">Response type.</typeparam>
public interface IHttpHeaderInjector<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Get http header for http request.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Key and value for http header.</returns>
    ValueTask<(string, string)?> GetHeadersAsync(CancellationToken cancellationToken);
}