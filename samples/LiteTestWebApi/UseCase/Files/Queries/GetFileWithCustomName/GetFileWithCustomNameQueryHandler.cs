using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Files.Queries.GetFileWithCustomName;

public class GetFileWithCustomNameQueryHandler : IRequestHandler<GetFileWithCustomNameQuery, FileResponse>
{
    public ValueTask<FileResponse> HandleAsync(GetFileWithCustomNameQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult<FileResponse>(new FileResponse("test"u8.ToArray(), "textfile.txt"));
    }
}