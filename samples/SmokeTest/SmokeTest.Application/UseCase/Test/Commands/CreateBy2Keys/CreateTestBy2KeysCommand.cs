using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Commands.CreateBy2Keys;

[Suffix("create")]
public class CreateTestBy2KeysCommand : KeyRequest<int, int>, IRequest<CreateTestBy2KeysResponse>
{
    public required string TestData { get; init; }
}