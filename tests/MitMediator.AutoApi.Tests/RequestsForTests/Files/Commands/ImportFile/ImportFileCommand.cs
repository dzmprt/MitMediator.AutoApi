using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFile;

[ExcludeFromCodeCoverage]
public class ImportFileCommand : FileRequest, IRequest<FileStreamResponse>
{
}