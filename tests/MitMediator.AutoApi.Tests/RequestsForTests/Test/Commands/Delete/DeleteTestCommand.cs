using System.Diagnostics.CodeAnalysis;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.Delete;

[ExcludeFromCodeCoverage]
public class DeleteTestCommand : IRequest
{
    public string TestData { get; init; }
}