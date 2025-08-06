using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Commands.Create;

public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, CreateTestResponse>
{
    public ValueTask<CreateTestResponse> HandleAsync(CreateTestCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(new CreateTestResponse
        {
            Value = $"Result from {nameof(CreateTestCommandHandler)}, TestData: {request.TestData}"
        });
    }
}