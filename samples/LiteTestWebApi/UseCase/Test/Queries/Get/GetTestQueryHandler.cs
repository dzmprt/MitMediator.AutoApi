using LiteTestWebApi.UseCase.Test.Queries.GetWithSuffix;
using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.Get;

public class GetTestQueryHandler : IRequestHandler<GetTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestQueryHandler)}, TestData: {request.TestData}");
    }
}