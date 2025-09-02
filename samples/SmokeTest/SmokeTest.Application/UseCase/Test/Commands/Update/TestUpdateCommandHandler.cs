using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.Update;

public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, string>
{
    public ValueTask<string> HandleAsync(UpdateTestCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(UpdateTestCommandHandler)}, TestData: {request.TestData}");
    }
}