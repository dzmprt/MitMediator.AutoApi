using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Files.Commands.UpdateFile;


[AutoApi(httpMethodType: HttpMethodType.Post, customPattern:"files/update")]
public class UpdateFileCommand : FileRequest, IRequest<byte[]>
{
}