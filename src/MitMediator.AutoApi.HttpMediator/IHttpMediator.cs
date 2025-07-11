namespace MitMediator.AutoApi.HttpMediator;

/// <summary>
/// Defines a mediator to encapsulate request/response and http requests.
/// </summary>
public interface IHttpMediator
{
    /// <summary>
    /// Send http request from command.
    /// </summary>
    /// <param name="request">Request object.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <typeparam name="TRequest"><see cref="TRequest"/></typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <returns>Response from server api.</returns>
    ValueTask<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>;
    
    /// <summary>
    /// Send http request from command.
    /// </summary>
    /// <param name="request">Request object.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <typeparam name="TRequest"><see cref="TRequest"/></typeparam>
    /// <returns><see cref="Unit"/></returns>
    ValueTask<Unit> SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest;
}