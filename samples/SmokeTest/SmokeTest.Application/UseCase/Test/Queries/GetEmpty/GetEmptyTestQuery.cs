using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetEmpty;

public class GetEmptyTestQuery : IRequest<string>
{
    public required string TestData { get; init; }
}