using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFileWithKey3Command;

[ExcludeFromCodeCoverage]
public class ImportFileWithKey3Command : FileRequest, IRequest<FileStreamResponse>, IKeyRequest<int, int, int>
{
	public int Key1 { get; init; }
	public int Key2 { get; init; }
	public int Key3 { get; init; }
}