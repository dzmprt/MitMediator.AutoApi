using System.Text.Json.Serialization;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Queries.GetList;

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