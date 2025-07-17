using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Queries.GetFilePng;

[ExcludeFromCodeCoverage]
[AutoApi(customResponseContentType:"image/png")]
public class GetFilePngQuery: IRequest<byte[]>
{
}