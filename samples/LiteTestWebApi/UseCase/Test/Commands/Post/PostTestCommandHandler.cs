using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.Post;

public class PostTestCommandHandler : IRequestHandler<PostTestCommand, string>
{
    public ValueTask<string> HandleAsync(PostTestCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(PostTestCommandHandler)}, TestData: {request.TestData}");
    }
}