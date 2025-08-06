using MitMediator;

namespace SmokeTestWebApi.UseCase.Test.Queries.GetList;

public class GetListQueryHandler : IRequestHandler<GetListQuery, GetListResponse>
{
    public ValueTask<GetListResponse> HandleAsync(GetListQuery request, CancellationToken cancellationToken)
    {
        var result = Enumerable.Range(0, 10).Select(x => $"Record #{x} from GetListQueryHandler").ToArray();
        var totalCount = result.Length * 10;
        var response = new GetListResponse
        {
            Items = result,
        };
        response.SetTotalCount(totalCount);
        return new ValueTask<GetListResponse>(response);
    }
}