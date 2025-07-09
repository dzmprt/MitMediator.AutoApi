using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey4;

public class DeleteTestBy4KeysCommandHandler : IRequestHandler<DeleteTestBy4KeysCommand>
{
    public ValueTask<Unit> HandleAsync(DeleteTestBy4KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(DeleteTestBy4KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
        return ValueTask.FromResult(Unit.Value);
    }
}