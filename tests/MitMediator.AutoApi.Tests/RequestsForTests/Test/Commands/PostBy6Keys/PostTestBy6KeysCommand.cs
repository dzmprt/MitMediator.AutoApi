using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.PostBy6Keys;

[ExcludeFromCodeCoverage]
public class PostTestBy6KeysCommand : IRequest<string>, IKeyRequest<int, int, int, int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    public int Key4 { get; init; }
    public int Key5 { get; init; }
    public int Key6 { get; init; }
    
    public string TestData { get; init; }
}