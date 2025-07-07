using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey6;

public class TestGetByKey6QueryHandler : IRequestHandler<TestGetByKey6Query, string>
{
    public ValueTask<string> HandleAsync(TestGetByKey6Query request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestGetByKey6QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}");
    }
}