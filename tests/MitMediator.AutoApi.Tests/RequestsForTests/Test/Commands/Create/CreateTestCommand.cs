using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.Create;

[ExcludeFromCodeCoverage]
[AutoApi(patternSuffix:"create")]
public class CreateTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}