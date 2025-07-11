using System.Diagnostics.CodeAnalysis;
using MitMediator;

namespace RequestsForTests.Test.Commands.Post;

[ExcludeFromCodeCoverage]
public class PostTestCommand : IRequest<string>
{
    public string TestData { get; init; }
}