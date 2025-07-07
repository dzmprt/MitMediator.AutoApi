using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateByKey;

public class TestCreateByKeyCommandHandler : IRequestHandler<TestCreateByKeyCommand, string>
{
    public ValueTask<string> HandleAsync(TestCreateByKeyCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(TestCreateByKeyCommandHandler)}, TestData: {request.TestData}, key: {request.Key}");
    }
}