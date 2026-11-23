using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Commands.CreateBy2Keys;

[Suffix("create")]
public class CreateTestBy2KeysCommand : IKeyRequest<int, int>, IRequest<CreateTestBy2KeysResponse>
{
    public required string TestData { get; init; }
    public int Key1 { get; init; }
    public int Key2 { get; init; }
}