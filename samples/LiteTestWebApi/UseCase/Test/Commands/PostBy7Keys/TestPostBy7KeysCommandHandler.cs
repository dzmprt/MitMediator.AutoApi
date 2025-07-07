using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.PostBy7Keys;

public class TestPostBy7KeysCommandHandler : IRequestHandler<TestPostBy7KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestPostBy7KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestPostBy7KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}, {request.Key7}");
    }
}