using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey5;

public class TestDeleteBy5KeysCommandHandler : IRequestHandler<TestDeleteBy5KeysCommand>
{
    public ValueTask<Unit> HandleAsync(TestDeleteBy5KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(TestDeleteBy5KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}");
        return ValueTask.FromResult(Unit.Value);
    }
}