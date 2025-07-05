using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKeyWithCastomPath;

public class TestGetByKeyWithCustomPathQueryHandler : IRequestHandler<TestGetByKeyWithCustomPathQuery, string>
{
    public ValueTask<string> HandleAsync(TestGetByKeyWithCustomPathQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestGetByKeyWithCustomPathQuery)} by key {request.Key}");
    }
}