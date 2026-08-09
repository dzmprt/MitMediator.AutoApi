using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.DeleteByKey2;

[ExcludeFromCodeCoverage]
public class DeleteTestBy2KeysCommand : IRequest, IKeyRequest<int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }

    public string TestData { get; init; }
}