using System.Text.Json;
using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetWithQueryParams;

public class GetTestWithQueryParamsQueryHandler : IRequestHandler<GetTestWithQueryParamsQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestWithQueryParamsQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Key: {request.Key}, query params: {JsonSerializer.Serialize(request)}");
    }
}