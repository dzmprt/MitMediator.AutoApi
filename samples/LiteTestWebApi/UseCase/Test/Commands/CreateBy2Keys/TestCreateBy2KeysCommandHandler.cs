using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy2Keys;

public class TestCreateBy2KeysCommandHandler : IRequestHandler<TestCreateBy2KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestCreateBy2KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestCreateBy2KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}");
    }
}