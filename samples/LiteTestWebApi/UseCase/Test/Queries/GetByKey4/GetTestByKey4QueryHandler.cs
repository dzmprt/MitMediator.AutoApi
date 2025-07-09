using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey4;

public class GetTestByKey4QueryHandler : IRequestHandler<GetTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKey4QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
    }
}