using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy2Keys;

public class CreateTestBy2KeysCommandHandler : IRequestHandler<CreateTestBy2KeysCommand, string>
{
    public ValueTask<string> HandleAsync(CreateTestBy2KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(CreateTestBy2KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}");
    }
}