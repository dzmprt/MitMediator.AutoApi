using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy3Keys;

public class TestUpdateBy3KeysCommandHandler : IRequestHandler<TestUpdateBy3KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestUpdateBy3KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestUpdateBy3KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}");
    }
}