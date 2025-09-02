using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetEmpty;

public class GetEmptyTestQuery : IRequest<string>
{
    public string TestData { get; init; }
}