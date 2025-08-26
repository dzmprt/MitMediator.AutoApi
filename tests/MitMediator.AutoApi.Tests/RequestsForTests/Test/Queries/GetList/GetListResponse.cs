using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetList;

[ExcludeFromCodeCoverage]
public class GetListResponse : ITotalCount
{
    public string[] Items { get; init; }

    internal int _totalCount;

    public int GetTotalCount() => _totalCount;

    public void SetTotalCount(int totalCount)
    {
        _totalCount = totalCount;
    }
}