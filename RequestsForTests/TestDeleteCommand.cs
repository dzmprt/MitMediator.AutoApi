using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests;

[ExcludeFromCodeCoverage]
[Delete("DELETE", "v1", $"Just {nameof(DeleteAttribute)} test")]
public class TestDeleteCommand : IRequest
{
    public string TestData { get; init; }
}