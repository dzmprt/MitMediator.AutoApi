using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey3;

public class TestGetByKey3QueryHandler : IRequestHandler<TestGetByKey3Query, string>
{
    public ValueTask<string> HandleAsync(TestGetByKey3Query request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestGetByKey3QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}");
    }
}