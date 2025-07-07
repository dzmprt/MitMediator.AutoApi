using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests;

[ExcludeFromCodeCoverage]
[Post("POST", "v1", $"Just {nameof(PostAttribute)} test")]
public class TestPostCommand : IRequest<string>
{
    public string TestData { get; init; }
}