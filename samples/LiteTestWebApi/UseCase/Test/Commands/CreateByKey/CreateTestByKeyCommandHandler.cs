using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateByKey;

public class CreateTestByKeyCommandHandler : IRequestHandler<CreateTestByKeyCommand, string>
{
    public ValueTask<string> HandleAsync(CreateTestByKeyCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(CreateTestByKeyCommandHandler)}, TestData: {request.TestData}, key: {request.Key}");
    }
}