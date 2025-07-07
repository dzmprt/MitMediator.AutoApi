using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey6;

public class TestDeleteBy6KeysCommandHandler : IRequestHandler<TestDeleteBy6KeysCommand>
{
    public ValueTask<Unit> HandleAsync(TestDeleteBy6KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(TestDeleteBy6KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}");
        return ValueTask.FromResult(Unit.Value);
    }
}