using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.UpdateByKey;

public class UpdateTestByStringKeyCommandHandler : IRequestHandler<UpdateTestByStringKeyCommand, string>
{
    public ValueTask<string> HandleAsync(UpdateTestByStringKeyCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(request.Key);
    }
}