using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey4;

public class TestGetByKey4QueryHandler : IRequestHandler<TestGetByKey4Query, string>
{
    public ValueTask<string> HandleAsync(TestGetByKey4Query request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestGetByKey4QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
    }
}