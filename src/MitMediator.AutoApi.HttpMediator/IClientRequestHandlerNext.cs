namespace MitMediator.AutoApi.HttpMediator;

public interface IClientRequestHandlerNext<in TRequest, TResponse>
{
    ValueTask<TResponse> InvokeAsync(TRequest newRequest, CancellationToken cancellationToken);
}