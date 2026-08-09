using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFileWithKey6Command;

[ExcludeFromCodeCoverage]
public class ImportFileWithKey6Command : FileRequest, IRequest<FileStreamResponse>, IKeyRequest<int, int, int, int, int, int>
{
	public int Key1 { get; init; }
	public int Key2 { get; init; }
	public int Key3 { get; init; }
	public int Key4 { get; init; }
	public int Key5 { get; init; }
	public int Key6 { get; init; }
}