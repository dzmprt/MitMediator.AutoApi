using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFileWithKey2Command;

[ExcludeFromCodeCoverage]
public class ImportFileWithKey2Command : FileRequest, IRequest<FileStreamResponse>, IKeyRequest<int, int>
{
	public int Key1 { get; init; }
	public int Key2 { get; init; }
}