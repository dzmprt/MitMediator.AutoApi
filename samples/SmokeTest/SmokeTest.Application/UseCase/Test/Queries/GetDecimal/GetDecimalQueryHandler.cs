using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetDecimal;

public sealed class GetDecimalQueryHandler : IRequestHandler<GetDecimalQuery, decimal>
{
    public ValueTask<decimal> HandleAsync(GetDecimalQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(request.Key);
    }
}
