using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.UpdateFile;


[ExcludeFromCodeCoverage]
[Suffix("update")]
[Method(MethodType.Post)]
public class UpdateFileCommand : FileRequest, IRequest<byte[]>
{
}