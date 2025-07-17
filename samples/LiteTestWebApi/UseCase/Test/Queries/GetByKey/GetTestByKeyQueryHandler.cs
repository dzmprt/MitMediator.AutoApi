using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey;

public class GetTestByKeyQueryHandler : IRequestHandler<GetTestByKeyQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestByKeyQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKeyQueryHandler)} by key {request.Key}");
    }
}