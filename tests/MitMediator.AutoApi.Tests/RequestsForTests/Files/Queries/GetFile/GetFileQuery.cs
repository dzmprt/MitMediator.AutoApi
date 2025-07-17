using System.Diagnostics.CodeAnalysis;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Queries.GetFile;

[ExcludeFromCodeCoverage]
public class GetFileQuery: IRequest<byte[]>
{
    public string Base64String { get; set; }
}