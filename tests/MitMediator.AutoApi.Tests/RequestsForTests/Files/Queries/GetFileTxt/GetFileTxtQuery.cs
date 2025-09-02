using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Queries.GetFileTxt;

[ExcludeFromCodeCoverage]
[ResponseContentType("text/plain")]
public class GetFileTxtQuery: IRequest<byte[]>
{
    public string Base64String { get; set; }
}