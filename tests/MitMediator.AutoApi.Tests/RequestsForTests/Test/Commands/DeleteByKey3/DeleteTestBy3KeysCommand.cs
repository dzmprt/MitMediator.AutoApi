using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.DeleteByKey3;

[ExcludeFromCodeCoverage]
public class DeleteTestBy3KeysCommand : IRequest, IKeyRequest<int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }

    public string TestData { get; init; }
}