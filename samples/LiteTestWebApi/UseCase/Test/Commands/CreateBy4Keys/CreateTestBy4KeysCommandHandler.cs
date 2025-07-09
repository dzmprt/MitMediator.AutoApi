using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy4Keys;

public class CreateTestBy4KeysCommandHandler : IRequestHandler<CreateTestBy4KeysCommand, string>
{
    public ValueTask<string> HandleAsync(CreateTestBy4KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(CreateTestBy4KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
    }
}