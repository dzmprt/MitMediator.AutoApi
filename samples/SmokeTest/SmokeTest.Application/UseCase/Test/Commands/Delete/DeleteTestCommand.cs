using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.Delete;

public class DeleteTestCommand : IRequest
{
    public string TestData { get; init; }
}