using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey2;

public class TestGetByKey2QueryHandler : IRequestHandler<TestGetByKey2Query, string>
{
    public ValueTask<string> HandleAsync(TestGetByKey2Query request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestGetByKey2QueryHandler)} by keys {request.Key1}, {request.Key2}");
    }
}