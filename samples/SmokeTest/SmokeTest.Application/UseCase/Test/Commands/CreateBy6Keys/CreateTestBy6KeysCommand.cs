using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Commands.CreateBy6Keys;

[Suffix("create")]
public class CreateTestBy6KeysCommand : KeyRequest<int, int, int, int, int, int>, IRequest<CreateTestBy6KeysResponse>
{
    public required string TestData { get; init; }
}