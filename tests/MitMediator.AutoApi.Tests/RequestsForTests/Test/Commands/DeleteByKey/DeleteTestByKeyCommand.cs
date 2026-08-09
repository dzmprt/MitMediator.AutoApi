using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.DeleteByKey;

[ExcludeFromCodeCoverage]
public class DeleteTestByKeyCommand : IRequest, IKeyRequest<int>
{
    public int Key { get; init; }

    public string TestData { get; init; }
}