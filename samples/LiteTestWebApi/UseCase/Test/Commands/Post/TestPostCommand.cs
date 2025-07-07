using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.Post;

[Post("POST", "v1", $"Just {nameof(PostAttribute)} test")]
public class TestPostCommand : IRequest<string>
{
    public string TestData { get; init; }
}