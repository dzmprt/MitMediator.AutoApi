using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.Get;

public class GetTestQuery : IRequest<string>
{
    public string TestData { get; init; }
}