using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy3Keys;

public class CreateTestBy3KeysCommandHandler : IRequestHandler<CreateTestBy3KeysCommand, string>
{
    public ValueTask<string> HandleAsync(CreateTestBy3KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(CreateTestBy3KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}");
    }
}