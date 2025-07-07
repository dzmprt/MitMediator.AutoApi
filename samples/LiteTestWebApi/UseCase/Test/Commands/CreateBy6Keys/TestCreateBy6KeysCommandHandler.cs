using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy6Keys;

public class TestCreateBy6KeysCommandHandler : IRequestHandler<TestCreateBy6KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestCreateBy6KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestCreateBy6KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}");
    }
}