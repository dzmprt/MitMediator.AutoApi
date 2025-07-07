using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy4Keys;

public class TestCreateBy4KeysCommandHandler : IRequestHandler<TestCreateBy4KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestCreateBy4KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestCreateBy4KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
    }
}