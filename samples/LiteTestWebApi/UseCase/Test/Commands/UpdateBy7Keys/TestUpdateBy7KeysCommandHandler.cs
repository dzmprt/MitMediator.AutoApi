using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy7Keys;

public class TestUpdateBy7KeysCommandHandler : IRequestHandler<TestUpdateBy7KeysCommand, string>
{
    public ValueTask<string> HandleAsync(TestUpdateBy7KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestUpdateBy7KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}, {request.Key7}");
    }
}