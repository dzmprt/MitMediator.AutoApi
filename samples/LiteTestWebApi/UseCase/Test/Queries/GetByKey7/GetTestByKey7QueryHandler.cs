using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey7;

public class GetTestByKey7QueryHandler : IRequestHandler<GetTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKey7QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}, {request.Key7}");
    }
}