using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Files.Queries.GetFilePng;

[ExcludeFromCodeCoverage]
[AutoApi(customResponseContentType:"image/png")]
public class GetFilePngQuery: IRequest<byte[]>
{
}