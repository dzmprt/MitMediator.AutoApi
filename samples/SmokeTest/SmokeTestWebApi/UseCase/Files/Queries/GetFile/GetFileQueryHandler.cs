using MitMediator;

namespace SmokeTestWebApi.UseCase.Files.Queries.GetFile;

public class GetFileQueryHandler: IRequestHandler<GetFileQuery, byte[]>
{
    public ValueTask<byte[]> HandleAsync(GetFileQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(Convert.FromBase64String(request.Base64String));
    }
}