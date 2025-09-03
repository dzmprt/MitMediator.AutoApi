using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Queries.GetFilePng;

[ExcludeFromCodeCoverage]
[ResponseContentType("image/png")]
public class GetFilePngQuery: IRequest<byte[]>
{
}