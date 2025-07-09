using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy3Keys;

public class UpdateTestBy3KeysCommandHandler : IRequestHandler<UpdateTestBy3KeysCommand, string>
{
    public ValueTask<string> HandleAsync(UpdateTestBy3KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(UpdateTestBy3KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}");
    }
}