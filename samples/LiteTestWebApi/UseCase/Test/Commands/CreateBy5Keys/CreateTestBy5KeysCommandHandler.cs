using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy5Keys;

public class CreateTestBy5KeysCommandHandler : IRequestHandler<CreateTestBy5KeysCommand, string>
{
    public ValueTask<string> HandleAsync(CreateTestBy5KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(CreateTestBy5KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}");
    }
}