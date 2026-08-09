using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.PostBy2Keys;

[ExcludeFromCodeCoverage]
public class PostTestBy2KeysCommand : IRequest<string>, IKeyRequest<int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    
    public string TestData { get; init; }
}