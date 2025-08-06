using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Queries.GetWithSuffix;

public class GetTestWithSuffixQuery : IRequest<string>
{
    public string TestData { get; init; }
}