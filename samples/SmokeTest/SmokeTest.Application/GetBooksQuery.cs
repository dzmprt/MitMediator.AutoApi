using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application;

[Version("")]
public class ImportDocumentWordCommand : FileRequest, IRequest<FileResponse>
{
}