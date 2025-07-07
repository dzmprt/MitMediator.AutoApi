using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[ExcludeFromCodeCoverage]
[Update("PUT", "v1", $"Just {nameof(CreateAttribute)} test")]
public class TestUpdateCommand : IRequest<string>
{
    public string TestData { get; init; }
}