using MitMediator;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Commands.Create;

[Suffix("create")]
public class CreateTestCommand : IRequest<CreateTestResponse>
{
    public required string TestData { get; init; }
}