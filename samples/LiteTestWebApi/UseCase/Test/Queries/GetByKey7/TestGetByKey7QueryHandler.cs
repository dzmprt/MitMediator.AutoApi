using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey7;

public class TestGetByKey7QueryHandler : IRequestHandler<TestGetByKey7Query, string>
{
    public ValueTask<string> HandleAsync(TestGetByKey7Query request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestGetByKey7QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}, {request.Key7}");
    }
}