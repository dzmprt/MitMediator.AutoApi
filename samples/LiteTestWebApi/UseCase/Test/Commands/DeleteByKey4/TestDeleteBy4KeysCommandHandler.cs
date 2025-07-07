using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey4;

public class TestDeleteBy4KeysCommandHandler : IRequestHandler<TestDeleteBy4KeysCommand>
{
    public ValueTask<Unit> HandleAsync(TestDeleteBy4KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(TestDeleteBy4KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
        return ValueTask.FromResult(Unit.Value);
    }
}