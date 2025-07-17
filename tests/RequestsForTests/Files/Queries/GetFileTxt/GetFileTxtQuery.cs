using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Files.Queries.GetFileTxt;

[ExcludeFromCodeCoverage]
[AutoApi(customResponseContentType:"text/plain")]
public class GetFileTxtQuery: IRequest<byte[]>
{
    public string Base64String { get; set; }
}