using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[ExcludeFromCodeCoverage]
[Post("POST", "v1", $"Just {nameof(PostAttribute)} test")]
public class TestPostCommand : IRequest<string>
{
    public string TestData { get; init; }
}