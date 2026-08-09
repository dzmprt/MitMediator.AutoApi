using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.CreateBy5Keys;

[ExcludeFromCodeCoverage]
[Suffix("by5-keys/create")]
public class CreateTestBy5KeysCommand : IRequest<string>, IKeyRequest<int, int, int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    public int Key4 { get; init; }
    public int Key5 { get; init; }
    
    public string TestData { get; init; }
}