using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Commands.PostByKey;

public class PostTestByKeyCommandHandler : IRequestHandler<PostTestByKeyCommand, string>
{
    public ValueTask<string> HandleAsync(PostTestByKeyCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(PostTestByKeyCommandHandler)}, TestData: {request.TestData}, key: {request.Key}");
    }
}