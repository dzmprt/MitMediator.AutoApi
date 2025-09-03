using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Queries.GetList;

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