using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[ExcludeFromCodeCoverage]
[Create("CREATE", "v1", $"Just {nameof(CreateAttribute)} test")]
public class TestCreateCommand : IRequest<string>
{
    public string TestData { get; init; }
}