using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.Post;

public class TestPostCommandHandler : IRequestHandler<TestPostCommand, string>
{
    public ValueTask<string> HandleAsync(TestPostCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestPostCommandHandler)}, TestData: {request.TestData}");
    }
}