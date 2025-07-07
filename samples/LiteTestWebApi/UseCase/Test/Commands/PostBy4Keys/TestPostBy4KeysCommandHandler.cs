using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy4Keys;

public class TestPostBy4KeysCommandHandler : IRequestHandler<TestPostBy4KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestPostBy4KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestPostBy4KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
    }
}