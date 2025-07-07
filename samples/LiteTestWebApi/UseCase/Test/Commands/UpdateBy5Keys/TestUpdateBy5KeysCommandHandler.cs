using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy5Keys;

public class TestUpdateBy5KeysCommandHandler : IRequestHandler<TestUpdateBy5KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestUpdateBy5KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestUpdateBy5KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}");
    }
}