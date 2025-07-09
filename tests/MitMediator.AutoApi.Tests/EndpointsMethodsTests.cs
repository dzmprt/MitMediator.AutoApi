using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using GetTestQuery = RequestsForTests.Test.Queries.GetByKey3.GetTestQuery;

namespace MitMediator.AutoApi.Tests;

public class EndpointsMethodsTests
{
    [Fact]
    public async Task WithBodyAnd3Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<GetTestQuery, string>(
                It.IsAny<GetTestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new GetTestQuery();

        var del = EndpointsMethods
            .WithBodyAnd3Keys<GetTestQuery, string, int, int, int>();
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result.Result);
    }
}
