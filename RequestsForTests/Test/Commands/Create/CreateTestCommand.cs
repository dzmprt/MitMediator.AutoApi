using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Test.Commands.Create;

[ExcludeFromCodeCoverage]
[AutoApi(patternSuffix:"create")]
public class CreateTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}