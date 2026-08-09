using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.DeleteByKey4;

[ExcludeFromCodeCoverage]
public class DeleteTestBy4KeysCommand : IRequest, IKeyRequest<int, int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    public int Key4 { get; init; }

    public string TestData { get; init; }
}