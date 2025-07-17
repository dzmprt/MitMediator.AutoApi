using System.Diagnostics.CodeAnalysis;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.Post;

[ExcludeFromCodeCoverage]
public class PostTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}