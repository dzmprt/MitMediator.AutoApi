using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Queries.GetWithSuffix;

public class GetTestWithSuffixQuery : IRequest<string>
{
    public string TestData { get; init; }
}