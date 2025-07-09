using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey3;

public class DeleteTestBy3KeysCommandHandler : IRequestHandler<DeleteTestBy3KeysCommand>
{
    public ValueTask<Unit> HandleAsync(DeleteTestBy3KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(DeleteTestBy3KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}, {request.Key3}");
        return ValueTask.FromResult(Unit.Value);
    }
}