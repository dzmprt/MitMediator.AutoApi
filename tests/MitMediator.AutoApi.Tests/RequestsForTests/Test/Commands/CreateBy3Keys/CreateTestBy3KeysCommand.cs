using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.CreateBy3Keys;

[ExcludeFromCodeCoverage]
[Suffix("by3-keys/create")]
public struct CreateTestBy3KeysCommand : IRequest<string>, IKeyRequest<int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    
    public string TestData { get; init; }
}