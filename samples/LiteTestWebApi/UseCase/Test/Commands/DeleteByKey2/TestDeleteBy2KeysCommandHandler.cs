using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey2;

public class TestDeleteBy2KeysCommandHandler : IRequestHandler<TestDeleteBy2KeysCommand>
{
    public ValueTask<Unit> HandleAsync(TestDeleteBy2KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(TestDeleteBy2KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}");
        return ValueTask.FromResult(Unit.Value);
    }
}