using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.Get;

public class TestGetQueryHandler : IRequestHandler<TestGetQuery, string>
{
    public ValueTask<string> HandleAsync(TestGetQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestGetQueryHandler)}, TestData: {request.TestData}");
    }
}