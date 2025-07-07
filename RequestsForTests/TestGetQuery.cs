using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests;

[ExcludeFromCodeCoverage]
[Get("GET", "v1", $"Just {nameof(GetAttribute)} test")]
public class TestGetQuery : IRequest<string>
{
    public string TestData { get; init; }
}