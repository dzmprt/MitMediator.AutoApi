using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Queries.GetWithSuffix;

public class GetTestWithSuffixQueryHandler : IRequestHandler<GetTestWithSuffixQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestWithSuffixQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestWithSuffixQueryHandler)}, TestData: {request.TestData}");
    }
}