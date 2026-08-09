using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetByKey5;

[ExcludeFromCodeCoverage]
public class GetTestQuery : IRequest<string>, IKeyRequest<int, int, int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    public int Key4 { get; init; }
    public int Key5 { get; init; }
}