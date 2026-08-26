using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetList;

[ExcludeFromCodeCoverage]
public class GetListResponse : ITotalCount
{
    public string[] Items { get; init; }

    public long TotalCount { get; init; }
}