using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Queries.GetList;

public class GetListResponse : ITotalCount
{
    public string[] Items { get; init; }

    public long TotalCount { get; init; }
}