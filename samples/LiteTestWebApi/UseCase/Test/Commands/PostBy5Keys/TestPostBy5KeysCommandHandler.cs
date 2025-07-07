using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy5Keys;

public class TestPostBy5KeysCommandHandler : IRequestHandler<TestPostBy5KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestPostBy5KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestPostBy5KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}");
    }
}