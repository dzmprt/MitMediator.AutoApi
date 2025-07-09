using GetTestQuery = RequestsForTests.Test.Queries.GetByKey3.GetTestQuery;

namespace MitMediator.AutoApi.Tests;

public class HelpersTests
{
    [Fact]
    public void GetKeyType_ValidRequest_ReturnsGenericType()
    {
        var result = Helpers.GetKeyType(typeof(RequestsForTests.Test.Queries.GetByKey.GetTestQuery));
        Assert.Equal(typeof(int), result);
    }

    [Fact]
    public void GetKeyType_InvalidType_Throws()
    {
        var ex = Assert.Throws<Exception>(() => Helpers.GetKeyType(typeof(object)));
        Assert.Contains("must implement IKeyRequest<>", ex.Message);
    }

    [Fact]
    public void IsKeyRequest_RecognizesCorrectInterface()
    {
        Assert.True(Helpers.IsKeyRequest(typeof(GetTestQuery)));
        Assert.False(Helpers.IsKeyRequest(typeof(object)));
    }

    [Fact]
    public void GetResponseType_ReturnsExpectedGenericArgument()
    {
        var result = Helpers.GetResponseType(typeof(RequestsForTests.Test.Queries.Get.GetTestQuery));
        Assert.Equal(typeof(string), result);
    }
}
