using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.CreateBy6Keys;

[ExcludeFromCodeCoverage]
[Suffix("by6-keys/create")]
public class CreateTestBy6KeysCommand : IRequest<string>, IKeyRequest<int, int, int, int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    public int Key4 { get; init; }
    public int Key5 { get; init; }
    public int Key6 { get; init; }
    
    public string TestData { get; init; }
}