using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy4Keys;

public class TestUpdateBy4KeysCommandHandler : IRequestHandler<TestUpdateBy4KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestUpdateBy4KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestUpdateBy4KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
    }
}