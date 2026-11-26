using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.UpdateByKey;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.UpdateBy2Keys;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.UpdateBy3Keys;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.UpdateBy4Keys;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.UpdateBy5Keys;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.UpdateBy6Keys;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Commands.UpdateBy7Keys;
using Moq;

namespace MitMediator.AutoApi.Tests.EndpointsMethodsTests;

public class WithBodyMethodsTests
{
    [Fact]
    public async Task WithBodyAndOneKey_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<UpdateTestByKeyCommand, string>(
                It.IsAny<UpdateTestByKeyCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new UpdateTestByKeyCommand { TestData = "payload" };

        var del = EndpointsMethods
            .WithBodyAnd1Key<UpdateTestByKeyCommand, string, int>(new RequestInfo(typeof(UpdateTestByKeyCommand)));
        
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(await result);
        mediatorMock.Verify(m => m.SendAsync<UpdateTestByKeyCommand, string>(
            It.Is<UpdateTestByKeyCommand>(value =>
                value.TestData == "payload" &&
                value.Key == 1),
            CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task WithBodyAnd2Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<UpdateTestBy2KeysCommand, string>(
                It.IsAny<UpdateTestBy2KeysCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new UpdateTestBy2KeysCommand { TestData = "payload" };

        var del = EndpointsMethods
            .WithBodyAnd2Keys<UpdateTestBy2KeysCommand, string, int, int>(new RequestInfo(typeof(UpdateTestBy2KeysCommand)));
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(await result);
        mediatorMock.Verify(m => m.SendAsync<UpdateTestBy2KeysCommand, string>(
            It.Is<UpdateTestBy2KeysCommand>(value =>
                value.TestData == "payload" &&
                value.Key1 == 1 &&
                value.Key2 == 2),
            CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task WithBodyAnd3Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<UpdateTestBy3KeysCommand, string>(
                It.IsAny<UpdateTestBy3KeysCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new UpdateTestBy3KeysCommand { TestData = "payload" };

        var del = EndpointsMethods
            .WithBodyAnd3Keys<UpdateTestBy3KeysCommand, string, int, int, int>(new RequestInfo(typeof(UpdateTestBy3KeysCommand)));
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(await result);
        mediatorMock.Verify(m => m.SendAsync<UpdateTestBy3KeysCommand, string>(
            It.Is<UpdateTestBy3KeysCommand>(value =>
                value.TestData == "payload" &&
                value.Key1 == 1 &&
                value.Key2 == 2 &&
                value.Key3 == 3),
            CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task WithBodyAnd4Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<UpdateTestBy4KeysCommand, string>(
                It.IsAny<UpdateTestBy4KeysCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new UpdateTestBy4KeysCommand { TestData = "payload" };

        var del = EndpointsMethods
            .WithBodyAnd4Keys<UpdateTestBy4KeysCommand, string, int, int, int, int>(new RequestInfo(typeof(UpdateTestBy4KeysCommand)));
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, 4, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(await result);
        mediatorMock.Verify(m => m.SendAsync<UpdateTestBy4KeysCommand, string>(
            It.Is<UpdateTestBy4KeysCommand>(value =>
                value.TestData == "payload" &&
                value.Key1 == 1 &&
                value.Key2 == 2 &&
                value.Key3 == 3 &&
                value.Key4 == 4),
            CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task WithBodyAnd5Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<UpdateTestBy5KeysCommand, string>(
                It.IsAny<UpdateTestBy5KeysCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new UpdateTestBy5KeysCommand { TestData = "payload" };

        var del = EndpointsMethods
            .WithBodyAnd5Keys<UpdateTestBy5KeysCommand, string, int, int, int, int, int>(new RequestInfo(typeof(UpdateTestBy5KeysCommand)));
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, 4, 5, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(await result);
        mediatorMock.Verify(m => m.SendAsync<UpdateTestBy5KeysCommand, string>(
            It.Is<UpdateTestBy5KeysCommand>(value =>
                value.TestData == "payload" &&
                value.Key1 == 1 &&
                value.Key2 == 2 &&
                value.Key3 == 3 &&
                value.Key4 == 4 &&
                value.Key5 == 5),
            CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task WithBodyAnd6Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<UpdateTestBy6KeysCommand, string>(
                It.IsAny<UpdateTestBy6KeysCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new UpdateTestBy6KeysCommand { TestData = "payload" };

        var del = EndpointsMethods
            .WithBodyAnd6Keys<UpdateTestBy6KeysCommand, string, int, int, int, int, int, int>(new RequestInfo(typeof(UpdateTestBy6KeysCommand)));
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, 4, 5, 6, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(await result);
        mediatorMock.Verify(m => m.SendAsync<UpdateTestBy6KeysCommand, string>(
            It.Is<UpdateTestBy6KeysCommand>(value =>
                value.TestData == "payload" &&
                value.Key1 == 1 &&
                value.Key2 == 2 &&
                value.Key3 == 3 &&
                value.Key4 == 4 &&
                value.Key5 == 5 &&
                value.Key6 == 6),
            CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task WithBodyAnd7Keys_ShouldCallMediatorCorrectly()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.SendAsync<UpdateTestBy7KeysCommand, string>(
                It.IsAny<UpdateTestBy7KeysCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var request = new UpdateTestBy7KeysCommand { TestData = "payload" };

        var del = EndpointsMethods
            .WithBodyAnd7Keys<UpdateTestBy7KeysCommand, string, int, int, int, int, int, int, int>(new RequestInfo(typeof(UpdateTestBy7KeysCommand)));
        var result = (ValueTask<IResult>)del.DynamicInvoke(request, 1, 2, 3, 4, 5, 6, 7, context, CancellationToken.None);

        Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>(await result);
        mediatorMock.Verify(m => m.SendAsync<UpdateTestBy7KeysCommand, string>(
            It.Is<UpdateTestBy7KeysCommand>(value =>
                value.TestData == "payload" &&
                value.Key1 == 1 &&
                value.Key2 == 2 &&
                value.Key3 == 3 &&
                value.Key4 == 4 &&
                value.Key5 == 5 &&
                value.Key6 == 6 &&
                value.Key7 == 7),
            CancellationToken.None), Times.Once);
    }
}