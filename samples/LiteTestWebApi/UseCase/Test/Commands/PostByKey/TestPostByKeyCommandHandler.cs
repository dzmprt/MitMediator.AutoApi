using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostByKey;

public class TestPostByKeyCommandHandler : IRequestHandler<TestPostByKeyCommand, string>
{
    public ValueTask<string> HandleAsync(TestPostByKeyCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestPostByKeyCommandHandler)}, TestData: {request.TestData}, key: {request.Key}");
    }
}