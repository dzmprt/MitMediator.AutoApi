namespace MitMediator.AutoApi.HttpMediator;

/// <summary>
/// Handler for a request.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response from the handler.</typeparam>
public interface IClientRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles a request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Response from the request.</returns>
    ValueTask<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}

/// <summary>
/// Handler for a request.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
public interface IClientRequestHandler<in TRequest> : IRequestHandler<TRequest, Unit> where TRequest : IRequest<Unit>;
