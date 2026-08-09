using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.CreateBy2Keys;

[ExcludeFromCodeCoverage]
[Suffix("by2-keys/create")]
public class CreateTestBy2KeysCommand : IRequest<string>, IKeyRequest<int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    
    public string TestData { get; init; }
}