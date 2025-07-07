using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy5Keys;

public class TestCreateBy5KeysCommandHandler : IRequestHandler<TestCreateBy5KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestCreateBy5KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestCreateBy5KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}");
    }
}