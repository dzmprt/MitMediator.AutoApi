using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetEmpty;

public class GetEmptyTestQueryHandler : IRequestHandler<GetEmptyTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetEmptyTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetEmptyTestQueryHandler)}, TestData: {request.TestData}");
    }
}