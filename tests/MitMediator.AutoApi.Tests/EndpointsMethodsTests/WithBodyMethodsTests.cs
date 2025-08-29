using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MitMediator.AutoApi.Tests.EndpointsMethodsTests;

public class WithBodyMethodsTests
{
    [Fact]
    public async Task WithBodyAndOneKey_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<RequestsForTests.Test.Queries.GetByKey.GetTestQuery, string>(
                It.IsAny<RequestsForTests.Test.Queries.GetByKey.GetTestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new RequestsForTests.Test.Queries.GetByKey.GetTestQuery();

        var del = EndpointsMethods
            .WithBodyAnd1Key<RequestsForTests.Test.Queries.GetByKey.GetTestQuery, string, int>();
        
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result.Result);
    }
    
    [Fact]
    public async Task WithBodyAnd2Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<RequestsForTests.Test.Queries.GetByKey2.GetTestQuery, string>(
                It.IsAny<RequestsForTests.Test.Queries.GetByKey2.GetTestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new RequestsForTests.Test.Queries.GetByKey2.GetTestQuery();

        var del = EndpointsMethods
            .WithBodyAnd2Keys<RequestsForTests.Test.Queries.GetByKey2.GetTestQuery, string, int, int>();
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result.Result);
    }
    
    [Fact]
    public async Task WithBodyAnd3Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<RequestsForTests.Test.Queries.GetByKey3.GetTestQuery, string>(
                It.IsAny<RequestsForTests.Test.Queries.GetByKey3.GetTestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new RequestsForTests.Test.Queries.GetByKey3.GetTestQuery();

        var del = EndpointsMethods
            .WithBodyAnd3Keys<RequestsForTests.Test.Queries.GetByKey3.GetTestQuery, string, int, int, int>();
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result.Result);
    }
    
    [Fact]
    public async Task WithBodyAnd4Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<RequestsForTests.Test.Queries.GetByKey4.GetTestQuery, string>(
                It.IsAny<RequestsForTests.Test.Queries.GetByKey4.GetTestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new RequestsForTests.Test.Queries.GetByKey4.GetTestQuery();

        var del = EndpointsMethods
            .WithBodyAnd4Keys<RequestsForTests.Test.Queries.GetByKey4.GetTestQuery, string, int, int, int, int>();
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, 4, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result.Result);
    }
    
    [Fact]
    public async Task WithBodyAnd5Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<RequestsForTests.Test.Queries.GetByKey5.GetTestQuery, string>(
                It.IsAny<RequestsForTests.Test.Queries.GetByKey5.GetTestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new RequestsForTests.Test.Queries.GetByKey5.GetTestQuery();

        var del = EndpointsMethods
            .WithBodyAnd5Keys<RequestsForTests.Test.Queries.GetByKey5.GetTestQuery, string, int, int, int, int, int>();
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, 4, 5, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result.Result);
    }
    
    [Fact]
    public async Task WithBodyAnd6Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<RequestsForTests.Test.Queries.GetByKey6.GetTestQuery, string>(
                It.IsAny<RequestsForTests.Test.Queries.GetByKey6.GetTestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new RequestsForTests.Test.Queries.GetByKey6.GetTestQuery();

        var del = EndpointsMethods
            .WithBodyAnd6Keys<RequestsForTests.Test.Queries.GetByKey6.GetTestQuery, string, int, int, int, int, int, int>();
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, 4, 5, 6, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result.Result);
    }
    
    [Fact]
    public async Task WithBodyAnd7Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<RequestsForTests.Test.Queries.GetByKey7.GetTestQuery, string>(
                It.IsAny<RequestsForTests.Test.Queries.GetByKey7.GetTestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new RequestsForTests.Test.Queries.GetByKey7.GetTestQuery();

        var del = EndpointsMethods
            .WithBodyAnd7Keys<RequestsForTests.Test.Queries.GetByKey7.GetTestQuery, string, int, int, int, int, int, int, int>();
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, 4, 5, 6, 7, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(result.Result);
    }
}