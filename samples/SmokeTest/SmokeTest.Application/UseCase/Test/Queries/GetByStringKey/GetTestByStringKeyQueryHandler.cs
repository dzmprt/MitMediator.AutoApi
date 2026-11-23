using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey;

public class GetTestByStringKeyQueryHandler : IRequestHandler<GetTestByStringKeyQuery, string>
{
    public ValueTask<string> HandleAsync(GetTestByStringKeyQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(request.Key);
    }
}