using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy3Keys;

public class TestCreateBy3KeysCommandHandler : IRequestHandler<TestCreateBy3KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestCreateBy3KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestCreateBy3KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}");
    }
}