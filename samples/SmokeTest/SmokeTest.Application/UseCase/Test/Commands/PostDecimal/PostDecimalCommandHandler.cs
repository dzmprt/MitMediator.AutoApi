using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.PostDecimal;

public sealed class PostDecimalCommandHandler : IRequestHandler<PostDecimalCommand, decimal>
{
    public ValueTask<decimal> HandleAsync(PostDecimalCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(request.Value);
    }
}
