using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetEmpty;

public class GetQueryHandler : IRequestHandler<GetQuery, string>
{
    public ValueTask<string> HandleAsync(GetQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetQueryHandler)}, TestData: {request.TestData}");
    }
}