using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.UpdateFile;


[ExcludeFromCodeCoverage]
[AutoApi(httpMethodType: HttpMethodType.Post, customPattern:"api/files/update")]
public class UpdateFileCommand : FileRequest, IRequest<byte[]>
{
}