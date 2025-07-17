namespace MitMediator.AutoApi.HttpMediator;

internal struct RequestPipeline<TRequest, TResponse> : IRequestHandlerNext<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerator<IPipelineBehavior<TRequest, TResponse>> _behaviors;
    private readonly IRequestHandler<TRequest, TResponse> _handler;

    public RequestPipeline(IEnumerator<IPipelineBehavior<TRequest, TResponse>> behaviors, IRequestHandler<TRequest, TResponse> handler)
    {
        _behaviors = behaviors;
        _handler = handler;
    }

    public ValueTask<TResponse> InvokeAsync(TRequest request, CancellationToken cancellationToken)
    {
        return _behaviors.MoveNext() ? _behaviors.Current.HandleAsync(request, this, cancellationToken) : _handler.HandleAsync(request, cancellationToken);
    }
}