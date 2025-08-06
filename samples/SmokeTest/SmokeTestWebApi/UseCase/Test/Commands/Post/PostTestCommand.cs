using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.Post;

public class PostTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}