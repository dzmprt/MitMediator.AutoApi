using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetV2;

public class GetTestQueryHandler : IRequestHandler<GetTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestQueryHandler)} (v2), TestData: {request.TestData}");
    }
}