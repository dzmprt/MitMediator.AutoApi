using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.Delete;

public class DeleteTestCommand : IRequest
{
    public string TestData { get; init; }
}