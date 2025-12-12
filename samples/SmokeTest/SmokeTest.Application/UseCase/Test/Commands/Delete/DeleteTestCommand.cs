using MitMediator;

namespace SmokeTest.Application.UseCase.Test.Commands.Delete;

public class DeleteTestCommand : IRequest
{
    public required string TestData { get; init; }
}