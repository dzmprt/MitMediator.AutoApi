using System.Diagnostics.CodeAnalysis;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.Update;

[ExcludeFromCodeCoverage]
public class UpdateTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}