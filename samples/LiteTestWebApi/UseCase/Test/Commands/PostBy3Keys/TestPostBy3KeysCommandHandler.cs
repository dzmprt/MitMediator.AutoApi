using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy3Keys;

public class TestPostBy3KeysCommandHandler : IRequestHandler<TestPostBy3KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestPostBy3KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestPostBy3KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}");
    }
}