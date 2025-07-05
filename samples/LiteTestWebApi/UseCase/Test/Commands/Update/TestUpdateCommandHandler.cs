using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.Update;

public class TestUpdateCommandHandler : IRequestHandler<TestUpdateCommand, string>
{
    public ValueTask<string> HandleAsync(TestUpdateCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestUpdateCommandHandler)}, TestData: {request.TestData}");
    }
}