using MitMediator;
using SmokeTest.Application.UseCase.Test.Queries.GetByKey7;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey7WithDifferentTypes;

public class GetTestByKey7QueryHandler : IRequestHandler<GetByKey7WithDifferentTypesQuery, string>
{
    public ValueTask<string> HandleAsync(GetByKey7WithDifferentTypesQuery request, CancellationToken cancellationToken)
    {
        var response =
            $"Result from {nameof(GetTestByKey7QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}, {request.Key7}";
        return ValueTask.FromResult(response);
    }
}