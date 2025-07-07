using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy2Keys;

public class TestPostBy2KeysCommandHandler : IRequestHandler<TestPostBy2KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestPostBy2KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestPostBy2KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}");
    }
}