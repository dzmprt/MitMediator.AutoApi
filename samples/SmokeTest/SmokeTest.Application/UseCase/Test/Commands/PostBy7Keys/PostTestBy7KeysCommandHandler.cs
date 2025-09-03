using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.PostBy7Keys;

public class PostTestBy7KeysCommandHandler : IRequestHandler<PostTestBy7KeysCommand, string>
{
    public ValueTask<string> HandleAsync(PostTestBy7KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(PostTestBy7KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}, {request.Key7}");
    }
}