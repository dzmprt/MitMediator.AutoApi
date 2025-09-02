using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.CreateBy2Keys;

public class CreateTestBy2KeysCommandHandler : IRequestHandler<CreateTestBy2KeysCommand, CreateTestBy2KeysResponse>
{
    public ValueTask<CreateTestBy2KeysResponse> HandleAsync(CreateTestBy2KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(new CreateTestBy2KeysResponse
        {
            Value = $"Result from {nameof(CreateTestBy2KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}"
        });
    }
}