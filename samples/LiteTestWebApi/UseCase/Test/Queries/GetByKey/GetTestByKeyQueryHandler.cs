using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey;

public class GetTestByKeyQueryHandler : IRequestHandler<GetTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKeyQueryHandler)} by key {request.Key}");
    }
}