using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.Delete;

public class TestDeleteCommandHandler : IRequestHandler<TestDeleteCommand>
{
    public ValueTask<Unit> HandleAsync(TestDeleteCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(TestDeleteCommandHandler)}, TestData: {request.TestData}");
        return ValueTask.FromResult(Unit.Value);
    }
}