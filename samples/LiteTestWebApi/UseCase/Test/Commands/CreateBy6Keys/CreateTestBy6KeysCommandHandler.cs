using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateBy6Keys;

public class CreateTestBy6KeysCommandHandler : IRequestHandler<CreateTestBy6KeysCommand, string>
{
    public ValueTask<string> HandleAsync(CreateTestBy6KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(CreateTestBy6KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}");
    }
}