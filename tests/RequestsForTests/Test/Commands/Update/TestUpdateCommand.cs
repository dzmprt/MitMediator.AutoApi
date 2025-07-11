using System.Diagnostics.CodeAnalysis;
using MitMediator;

namespace RequestsForTests.Test.Commands.Update;

[ExcludeFromCodeCoverage]
public class UpdateTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}