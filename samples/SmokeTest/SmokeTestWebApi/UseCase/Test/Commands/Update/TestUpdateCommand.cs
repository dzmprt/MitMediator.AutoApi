using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.Update;

public class UpdateTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}