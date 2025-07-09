using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy4Keys;

public class PostTestBy4KeysCommandHandler : IRequestHandler<PostTestBy4KeysCommand, string>
{
    public ValueTask<string> HandleAsync(PostTestBy4KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(PostTestBy4KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
    }
}