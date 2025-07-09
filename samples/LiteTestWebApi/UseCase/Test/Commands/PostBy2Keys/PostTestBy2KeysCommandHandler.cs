using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy2Keys;

public class PostTestBy2KeysCommandHandler : IRequestHandler<PostTestBy2KeysCommand, string>
{
    public ValueTask<string> HandleAsync(PostTestBy2KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(PostTestBy2KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}");
    }
}