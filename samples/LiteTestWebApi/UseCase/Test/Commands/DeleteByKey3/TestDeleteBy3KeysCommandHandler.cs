using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey3;

public class TestDeleteBy3KeysCommandHandler : IRequestHandler<TestDeleteBy3KeysCommand>
{
    public ValueTask<Unit> HandleAsync(TestDeleteBy3KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(TestDeleteBy3KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}, {request.Key3}");
        return ValueTask.FromResult(Unit.Value);
    }
}