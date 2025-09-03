using MitMediator;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Queries.Get;

[Version("")]
public class GetTestQuery : IRequest<string>
{
    public string TestData { get; init; }
}