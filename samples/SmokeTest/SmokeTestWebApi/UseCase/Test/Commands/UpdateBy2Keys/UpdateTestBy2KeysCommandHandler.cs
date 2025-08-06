using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Commands.UpdateBy2Keys;

public class UpdateTestBy2KeysCommandHandler : IRequestHandler<UpdateTestBy2KeysCommand, string>
{
    public ValueTask<string> HandleAsync(UpdateTestBy2KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(UpdateTestBy2KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}");
    }
}