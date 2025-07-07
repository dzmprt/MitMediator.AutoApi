using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey7;

public class TestDeleteBy7KeysCommandHandler : IRequestHandler<TestDeleteBy7KeysCommand>
{
    public ValueTask<Unit> HandleAsync(TestDeleteBy7KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(TestDeleteBy7KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}, {request.Key7}");
        return ValueTask.FromResult(Unit.Value);
    }
}