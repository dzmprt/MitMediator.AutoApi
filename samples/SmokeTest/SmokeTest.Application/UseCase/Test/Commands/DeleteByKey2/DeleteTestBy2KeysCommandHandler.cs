using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.DeleteByKey2;

public class DeleteTestBy2KeysCommandHandler : IRequestHandler<DeleteTestBy2KeysCommand>
{
    public ValueTask<Unit> HandleAsync(DeleteTestBy2KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(DeleteTestBy2KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}");
        return ValueTask.FromResult(Unit.Value);
    }
}