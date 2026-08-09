using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.CreateByKey;

[ExcludeFromCodeCoverage]
[Suffix("by-key/create")]
public class CreateTestByKeyCommand : IRequest<string>, IKeyRequest<int>
{
    public int Key { get; init; }
    
    public string TestData { get; init; }
}