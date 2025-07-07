using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[ExcludeFromCodeCoverage]
[Get("get-model")]
public class GetRequestNoVersion : IRequest<string> { }