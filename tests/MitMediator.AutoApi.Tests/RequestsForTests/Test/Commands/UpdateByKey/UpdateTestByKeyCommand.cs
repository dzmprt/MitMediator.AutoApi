using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.UpdateByKey;

[ExcludeFromCodeCoverage]
public class UpdateTestByKeyCommand : IRequest<string>, IKeyRequest<int>
{
    public int Key { get; init; }
    
    public string TestData { get; init; }
}