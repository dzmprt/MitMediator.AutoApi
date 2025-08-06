using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.Create;

[AutoApi(patternSuffix:"create")]
public class CreateTestCommand : IRequest<CreateTestResponse>
{
    public string TestData { get; init; }
}