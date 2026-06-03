using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.PostDecimal;

public sealed class PostDecimalCommand : IRequest<decimal>
{
    public decimal Value { get; set; }
}
