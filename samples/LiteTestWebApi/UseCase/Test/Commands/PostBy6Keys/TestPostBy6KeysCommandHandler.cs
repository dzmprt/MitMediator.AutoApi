using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy6Keys;

public class TestPostBy6KeysCommandHandler : IRequestHandler<TestPostBy6KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestPostBy6KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestPostBy6KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}");
    }
}