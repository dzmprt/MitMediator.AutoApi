using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.Delete;

public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand>
{
    public ValueTask<Unit> HandleAsync(DeleteTestCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(DeleteTestCommandHandler)}, TestData: {request.TestData}");
        return ValueTask.FromResult(Unit.Value);
    }
}