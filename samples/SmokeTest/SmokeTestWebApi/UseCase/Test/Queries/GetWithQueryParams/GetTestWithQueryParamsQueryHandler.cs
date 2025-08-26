using System.Text.Json;
using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Queries.GetWithQueryParams;

public class GetTestWithQueryParamsQueryHandler : IRequestHandler<GetTestWithQueryParamsQuery, GetTestWithQueryParamsQuery>
{
    public ValueTask<GetTestWithQueryParamsQuery> HandleAsync(GetTestWithQueryParamsQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(request);
    }
}