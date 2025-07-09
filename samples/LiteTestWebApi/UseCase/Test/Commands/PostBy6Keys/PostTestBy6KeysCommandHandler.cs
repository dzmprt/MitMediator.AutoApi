using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy6Keys;

public class PostTestBy6KeysCommandHandler : IRequestHandler<PostTestBy6KeysCommand, string>
{
    public ValueTask<string> HandleAsync(PostTestBy6KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(PostTestBy6KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}");
    }
}