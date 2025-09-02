using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.UpdateByKey;

public class UpdateTestByKeyCommandHandler : IRequestHandler<UpdateTestByKeyCommand, string>
{
    public ValueTask<string> HandleAsync(UpdateTestByKeyCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(UpdateTestByKeyCommandHandler)}, TestData: {request.TestData}, key: {request.Key}");
    }
}