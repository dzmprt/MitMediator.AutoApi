using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.Create;

public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, string>
{
    public ValueTask<string> HandleAsync(CreateTestCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(CreateTestCommandHandler)}, TestData: {request.TestData}");
    }
}