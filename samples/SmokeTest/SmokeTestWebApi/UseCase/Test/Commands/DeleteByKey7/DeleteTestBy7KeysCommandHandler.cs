using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Commands.DeleteByKey7;

public class DeleteTestBy7KeysCommandHandler : IRequestHandler<DeleteTestBy7KeysCommand>
{
    public ValueTask<Unit> HandleAsync(DeleteTestBy7KeysCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Result from {nameof(DeleteTestBy7KeysCommandHandler)}, TestData: {request.TestData}, Keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}, {request.Key7}");
        return ValueTask.FromResult(Unit.Value);
    }
}