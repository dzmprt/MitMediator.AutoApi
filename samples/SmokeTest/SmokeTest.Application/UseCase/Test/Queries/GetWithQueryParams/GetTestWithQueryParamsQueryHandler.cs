using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetWithQueryParams;

public class GetTestWithQueryParamsQueryHandler : IRequestHandler<GetTestWithQueryParamsQuery, GetTestWithQueryParamsQuery>
{
    public ValueTask<GetTestWithQueryParamsQuery> HandleAsync(GetTestWithQueryParamsQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(request);
    }
}