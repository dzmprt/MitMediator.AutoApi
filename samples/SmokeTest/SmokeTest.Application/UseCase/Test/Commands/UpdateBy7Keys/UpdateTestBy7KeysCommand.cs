using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.UpdateBy7Keys;

public class UpdateTestBy7KeysCommand : KeyRequest<int, int, int, int, int, int, int>, IRequest<string>
{
    public required string TestData { get; init; }
}