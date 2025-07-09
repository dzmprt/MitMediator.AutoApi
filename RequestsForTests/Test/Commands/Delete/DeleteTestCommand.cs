using System.Diagnostics.CodeAnalysis;
using MitMediator;

namespace RequestsForTests.Test.Commands.Delete;

[ExcludeFromCodeCoverage]
public class DeleteTestCommand : IRequest
{
    public string TestData { get; init; }
}