using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey2;

public class GetTestByKey2QueryHandler : IRequestHandler<GetTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKey2QueryHandler)} by keys {request.Key1}, {request.Key2}");
    }
}