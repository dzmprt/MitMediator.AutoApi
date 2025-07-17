using MitMediator;

namespace LiteTestWebApi.UseCase.Files.Queries.GetFile;

public class GetFileQuery: IRequest<byte[]>
{
    public string Base64String { get; set; }
}