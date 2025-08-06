using MitMediator;

namespace SmokeTestWebApi.UseCase.Files.Queries.GetFile;

public class GetFileQuery: IRequest<byte[]>
{
    public string Base64String { get; set; }
}