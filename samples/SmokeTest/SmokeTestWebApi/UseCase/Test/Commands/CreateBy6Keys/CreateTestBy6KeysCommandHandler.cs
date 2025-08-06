using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Commands.CreateBy6Keys;

public class CreateTestBy6KeysCommandHandler : IRequestHandler<CreateTestBy6KeysCommand, CreateTestBy6KeysResponse>
{
    public ValueTask<CreateTestBy6KeysResponse> HandleAsync(CreateTestBy6KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            new CreateTestBy6KeysResponse()
            {
                Value = $"Result from {nameof(CreateTestBy6KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}, {request.Key6}",
            });
    }
}