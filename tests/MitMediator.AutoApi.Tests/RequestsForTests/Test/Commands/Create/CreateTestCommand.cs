using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.Create;

[ExcludeFromCodeCoverage]
[Suffix("create")]
public class CreateTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}