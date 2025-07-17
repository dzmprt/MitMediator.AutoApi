using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey2;

public class GetTestByKey2QueryHandler : IRequestHandler<GetTestByKey2Query, string>
{
    public ValueTask<string> HandleAsync(GetTestByKey2Query request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKey2QueryHandler)} by keys {request.Key1}, {request.Key2}");
    }
}