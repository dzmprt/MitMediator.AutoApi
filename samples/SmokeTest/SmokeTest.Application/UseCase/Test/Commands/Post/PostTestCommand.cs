using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.Post;

public class PostTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}