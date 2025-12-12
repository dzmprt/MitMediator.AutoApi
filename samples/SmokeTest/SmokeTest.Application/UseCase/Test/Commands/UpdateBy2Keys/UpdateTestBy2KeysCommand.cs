using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.UpdateBy2Keys;

public class UpdateTestBy2KeysCommand : KeyRequest<int, int>, IRequest<string>
{
    public required string TestData { get; init; }
}