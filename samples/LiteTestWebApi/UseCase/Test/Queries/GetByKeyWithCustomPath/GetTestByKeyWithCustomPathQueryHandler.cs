using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKeyWithCustomPath;

public class GetTestByKeyWithCustomPathQueryHandler : IRequestHandler<GetTestByKeyWithCustomPathQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestByKeyWithCustomPathQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(GetTestByKeyWithCustomPathQuery)} by key {request.Key}");
    }
}