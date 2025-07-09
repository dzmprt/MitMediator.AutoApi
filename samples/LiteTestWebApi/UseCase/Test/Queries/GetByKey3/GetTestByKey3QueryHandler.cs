using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey3;

public class GetTestByKey3QueryHandler : IRequestHandler<GetTestQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKey3QueryHandler)} by keys {request.Key1}, {request.Key2}, {request.Key3}");
    }
}