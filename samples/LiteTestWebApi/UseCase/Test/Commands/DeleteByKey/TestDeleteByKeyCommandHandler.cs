using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey;

public class TestDeleteByKeyCommandHandler : IRequestHandler<TestDeleteByKeyCommand>
{
    public ValueTask<Unit> HandleAsync(TestDeleteByKeyCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(TestDeleteByKeyCommand)}, TestData: {request.TestData}, Key: {request.Key}");
        return ValueTask.FromResult(Unit.Value);
    }
}