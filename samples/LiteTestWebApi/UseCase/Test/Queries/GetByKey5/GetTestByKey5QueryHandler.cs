using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey5;

public class GetTestByKey5QueryHandler : IRequestHandler<GetTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKey5QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}");
    }
}