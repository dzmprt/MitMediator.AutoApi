using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Commands.CreateByKey;

public class CreateTestByKeyCommandHandler : IRequestHandler<CreateTestByKeyCommand, CreateTestByKeyResponse>
{
    public ValueTask<CreateTestByKeyResponse> HandleAsync(CreateTestByKeyCommand request,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            new CreateTestByKeyResponse()
            {
                Value = $"Result from {nameof(CreateTestByKeyCommandHandler)}, TestData: {request.TestData}, key: {request.Key}"
            }
        );
    }
}