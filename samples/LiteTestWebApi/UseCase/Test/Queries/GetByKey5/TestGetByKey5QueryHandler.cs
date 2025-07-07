using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey5;

public class TestGetByKey5QueryHandler : IRequestHandler<TestGetByKey5Query, string>
{
    public ValueTask<string> HandleAsync(TestGetByKey5Query request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestGetByKey5QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}");
    }
}