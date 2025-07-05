using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateByKey;

public class TestUpdateByKeyCommandHandler : IRequestHandler<TestUpdateByKeyCommand, string>
{
    public ValueTask<string> HandleAsync(TestUpdateByKeyCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestUpdateByKeyCommandHandler)}, TestData: {request.TestData}, key: {request.Key}");
    }
}