using System.Diagnostics.CodeAnalysis;
using MitMediator;

namespace RequestsForTests.Files.Queries.GetFile;

[ExcludeFromCodeCoverage]
public class GetFileQuery: IRequest<byte[]>
{
    public string Base64String { get; set; }
}