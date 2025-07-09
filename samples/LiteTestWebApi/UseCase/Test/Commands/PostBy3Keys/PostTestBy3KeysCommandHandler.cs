using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy3Keys;

public class PostTestBy3KeysCommandHandler : IRequestHandler<PostTestBy3KeysCommand, string>
{
    public ValueTask<string> HandleAsync(PostTestBy3KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(PostTestBy3KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}");
    }
}