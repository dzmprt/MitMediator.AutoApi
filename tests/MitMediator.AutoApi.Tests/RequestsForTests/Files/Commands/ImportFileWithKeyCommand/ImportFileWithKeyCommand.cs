using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFileWithKeyCommand;

[ExcludeFromCodeCoverage]
public class ImportFileWithKeyCommand : FileRequest, IRequest<FileStreamResponse>, IKeyRequest<int>
{
	public int Key { get; init; }
}