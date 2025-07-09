using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey;

public class DeleteTestByKeyCommandHandler : IRequestHandler<DeleteTestByKeyCommand>
{
    public ValueTask<Unit> HandleAsync(DeleteTestByKeyCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(DeleteTestByKeyCommandHandler)}, TestData: {request.TestData}, Key: {request.Key}");
        return ValueTask.FromResult(Unit.Value);
    }
}