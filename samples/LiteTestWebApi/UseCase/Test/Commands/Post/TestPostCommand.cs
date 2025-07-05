using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.Post;

[Post(nameof(Test), "v1", $"Just {nameof(PostAttribute)} test", $"/v1/tests/post-with-200-result")]
public class TestPostCommand : IRequest<string>
{
    public string TestData { get; init; }
}