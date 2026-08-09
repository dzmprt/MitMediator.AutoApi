using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetByKey2;

[ExcludeFromCodeCoverage]
public class GetTestQuery : IRequest<string>, IKeyRequest<int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
}