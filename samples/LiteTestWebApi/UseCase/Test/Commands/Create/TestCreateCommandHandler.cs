using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.Create;

public class TestCreateCommandHandler : IRequestHandler<TestCreateCommand, string>
{
    public ValueTask<string> HandleAsync(TestCreateCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestCreateCommandHandler)}, TestData: {request.TestData}");
    }
}