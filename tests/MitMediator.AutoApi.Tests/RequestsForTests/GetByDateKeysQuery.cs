using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[Tag("tests")]
public class GetByDateKeysQuery : IRequest<string>, IKeyRequest<DateTime, DateTimeOffset>
{
    public void SetKey1(DateTime key)
    {
        throw new NotImplementedException();
    }

    public DateTime GetKey1()
    {
        throw new NotImplementedException();
    }

    public void SetKey2(DateTimeOffset key)
    {
        throw new NotImplementedException();
    }

    public DateTimeOffset GetKey2()
    {
        throw new NotImplementedException();
    }
}