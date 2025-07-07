using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[ExcludeFromCodeCoverage]
[Get("GET", "v1", $"Just {nameof(GetAttribute)} test")]
public class TestGetQuery : IRequest<string>
{
    public string TestData { get; init; }
}