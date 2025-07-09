using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey6;

public class GetTestByKey6QueryHandler : IRequestHandler<GetTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKey6QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}");
    }
}