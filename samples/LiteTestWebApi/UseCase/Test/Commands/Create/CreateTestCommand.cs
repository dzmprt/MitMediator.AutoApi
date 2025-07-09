using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.Create;

[AutoApi(patternSuffix:"create")]
public class CreateTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}