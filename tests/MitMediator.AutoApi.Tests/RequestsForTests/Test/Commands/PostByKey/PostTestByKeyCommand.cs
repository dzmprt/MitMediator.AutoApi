using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.PostByKey;

[ExcludeFromCodeCoverage]
public class PostTestByKeyCommand : IRequest<string>, IKeyRequest<int>
{
    public int Key { get; init; }
    
    public string TestData { get; init; }
}