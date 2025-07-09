using MitMediator;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy5Keys;

public class UpdateTestBy5KeysCommandHandler : IRequestHandler<UpdateTestBy5KeysCommand, string>
{
    public ValueTask<string> HandleAsync(UpdateTestBy5KeysCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(UpdateTestBy5KeysCommandHandler)}, TestData: {request.TestData}, keys: {request.Key1}, {request.Key2}, {request.Key3}, {request.Key4}, {request.Key5}");
    }
}