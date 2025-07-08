using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests;

[ExcludeFromCodeCoverage]
[Update("PUT", "v1", $"Just {nameof(CreateAttribute)} test")]
public class TestUpdateCommand : IRequest<string>
{
    public string TestData { get; init; }
}