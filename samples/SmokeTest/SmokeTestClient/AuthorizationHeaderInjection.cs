using MitMediator;
using MitMediator.AutoApi.HttpMediator;

namespace SmokeTestClient;

public class AuthorizationHeaderInjection<TRequest, TResponse> : IHttpHeaderInjector<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public ValueTask<(string, string)?> GetHeadersAsync(CancellationToken cancellationToken)
    {
        var result = ("Authorization", "Bearer 123");
        return ValueTask.FromResult<(string, string)?>(result);
    }
}