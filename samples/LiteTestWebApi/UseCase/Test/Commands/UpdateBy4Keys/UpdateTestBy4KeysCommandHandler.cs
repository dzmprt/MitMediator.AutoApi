using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy4Keys;

public class UpdateTestBy4KeysCommandHandler : IRequestHandler<UpdateTestBy4KeysCommand, string>
{
    public ValueTask<string> HandleAsync(UpdateTestBy4KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(UpdateTestBy4KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}");
    }
}