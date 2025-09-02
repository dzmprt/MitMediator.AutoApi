using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.Delete;

public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand>
{
    public ValueTask<Unit> HandleAsync(DeleteTestCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(DeleteTestCommandHandler)}, TestData: {request.TestData}");
        return ValueTask.FromResult(Unit.Value);
    }
}