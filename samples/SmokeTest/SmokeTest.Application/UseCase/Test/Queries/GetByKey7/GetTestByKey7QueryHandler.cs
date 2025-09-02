using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey7;

public class GetTestByKey7QueryHandler : IRequestHandler<GetTestByKey7Query, string>
{
    public ValueTask<string> HandleAsync(GetTestByKey7Query request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKey7QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}, {request.Key7}");
    }
}