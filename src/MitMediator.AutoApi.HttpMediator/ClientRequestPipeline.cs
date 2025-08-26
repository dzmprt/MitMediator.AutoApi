namespace MitMediator.AutoApi.HttpMediator;

internal struct ClientRequestPipeline<TRequest, TResponse> : IClientRequestHandlerNext<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerator<IClientPipelineBehavior<TRequest, TResponse>> _behaviors;
    private readonly IClientRequestHandler<TRequest, TResponse> _handler;

    public ClientRequestPipeline(IEnumerator<IClientPipelineBehavior<TRequest, TResponse>> behaviors, IClientRequestHandler<TRequest, TResponse> handler)
    {
        _behaviors = behaviors;
        _handler = handler;
    }

    public ValueTask<TResponse> InvokeAsync(TRequest request, CancellationToken cancellationToken)
    {
        return _behaviors.MoveNext() ? _behaviors.Current.HandleAsync(request, this, cancellationToken) : _handler.HandleAsync(request, cancellationToken);
    }
}