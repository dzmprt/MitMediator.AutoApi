namespace MitMediator.AutoApi.HttpMediator;

/// <summary>
/// Defines a client mediator.
/// </summary>
public interface IClientMediator
{
    /// <summary>
    /// Send request from command.
    /// </summary>
    /// <param name="request">Request object.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <typeparam name="TRequest"><see cref="TRequest"/></typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <returns>Response from server api.</returns>
    ValueTask<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>;
    
    /// <summary>
    /// Send request from command.
    /// </summary>
    /// <param name="request">Request object.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <typeparam name="TRequest"><see cref="TRequest"/></typeparam>
    /// <returns><see cref="Unit"/></returns>
    ValueTask<Unit> SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest;
}