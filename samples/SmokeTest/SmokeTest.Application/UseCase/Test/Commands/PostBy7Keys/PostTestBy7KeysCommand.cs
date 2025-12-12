using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.PostBy7Keys;

public class PostTestBy7KeysCommand : KeyRequest<int, int, int, int, int, int, int>, IRequest<string>
{
    public required string TestData { get; init; }
}