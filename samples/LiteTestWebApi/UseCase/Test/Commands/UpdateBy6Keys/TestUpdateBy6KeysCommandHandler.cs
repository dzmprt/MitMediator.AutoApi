using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy6Keys;

public class TestUpdateBy6KeysCommandHandler : IRequestHandler<TestUpdateBy6KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestUpdateBy6KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestUpdateBy6KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}");
    }
}