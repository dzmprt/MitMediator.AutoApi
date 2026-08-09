using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.UpdateBy2Keys;

[ExcludeFromCodeCoverage]
public class UpdateTestBy2KeysCommand : IRequest<string>, IKeyRequest<int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    
    public string TestData { get; init; }
}