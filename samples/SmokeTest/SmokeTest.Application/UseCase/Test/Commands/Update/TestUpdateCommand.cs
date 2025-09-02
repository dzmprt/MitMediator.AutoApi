using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.Update;

public class UpdateTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}