using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy2Keys;

public class TestUpdateBy2KeysCommandHandler : IRequestHandler<TestUpdateBy2KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestUpdateBy2KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestUpdateBy2KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}");
    }
}