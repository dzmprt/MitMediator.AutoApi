using MitMediator;

namespace LiteTestWebApi.UseCase.Files.Queries.GetTxtFile;

public class GetFileTxtQueryHandler: IRequestHandler<GetFileTxtQuery, byte[]>
{
    public ValueTask<byte[]> HandleAsync(GetFileTxtQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(Convert.FromBase64String(request.Base64String));
    }
}