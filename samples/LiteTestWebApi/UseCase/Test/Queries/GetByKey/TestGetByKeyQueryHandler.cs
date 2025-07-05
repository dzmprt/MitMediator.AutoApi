using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey;

public class TestGetByKeyQueryHandler : IRequestHandler<TestGetByKeyQuery, string>
{
    public ValueTask<string> HandleAsync(TestGetByKeyQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestGetByKeyQueryHandler)} by key {request.Key}");
    }
}