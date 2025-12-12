using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.PostBy2Keys;

public class PostTestBy2KeysCommand : KeyRequest<int, int>, IRequest<string>
{
    public required string TestData { get; init; }
}