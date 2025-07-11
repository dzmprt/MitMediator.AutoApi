using System.Net;
using Microsoft.Extensions.DependencyInjection;
using MitMediator;
using MitMediator.AutoApi;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.HttpMediator;
using Moq;
using Moq.Protected;

public class GetCommand : IRequest<string>
{
    public string Query => "test";
    public string GetKey() => "42";
}

public class PostCommand : IRequest<string>
{
    public string Payload => "data";
    public string GetKey1() => "A";
    public string GetKey2() => "B";
}

public class PutCommand : IRequest<string>
{
    public int Id => 5;
    public string GetKey() => "777";
}

public class DeleteCommand : IRequest<string>
{
    public string GetKey() => "99";
}

public class PostCreateCommand : IRequest<string>
{
    public string Name => "New";
    public string GetKey() => "123";
}

public class AutoCommand : IRequest<string> { }

public class EmptyResponseCommand : IRequest<string>
{
}

public class HttpRequestHandlerTests
{
    private static HttpRequestHandler<TReq, string> BuildHandler<TReq>(HttpMethodType method, string? responseContent,
        Action<HttpRequestMessage>? capture = null, bool customPattern = false, bool throwError = false)
        where TReq : IRequest<string>
    {
        var msgHandlerMock = new Mock<HttpMessageHandler>();
        msgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                if (throwError)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Bad" };
                }
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseContent ?? "") };
            });

        var httpClient = new HttpClient(msgHandlerMock.Object)
        {
            BaseAddress = new Uri("https://host")
        };

        var clientFactory = new Mock<IHttpClientFactory>();
        clientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var headerMock = new Mock<IHttpHeaderInjector<TReq, string>>();
        headerMock.Setup(h => h.GetHeadersAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(("X-Demo", "value"));

        var services = new ServiceCollection();
        services.AddSingleton(clientFactory.Object);
        services.AddSingleton<IEnumerable<IHttpHeaderInjector<TReq, string>>>(new[] { headerMock.Object });

        return new HttpRequestHandler<TReq, string>(services.BuildServiceProvider(), "https://base");
    }
    
    [Fact]
    public async Task Get_ShouldReturn_Response()
    {
        var handler = BuildHandler<GetCommand>(HttpMethodType.Get, "\"get-value\"");
        var result = await handler.HandleAsync(new GetCommand(), default);
        Assert.Equal("get-value", result);
    }

    [Fact]
    public async Task Post_ShouldReturn_Response()
    {
        var handler = BuildHandler<PostCommand>(HttpMethodType.Post, "\"post-value\"");
        var result = await handler.HandleAsync(new PostCommand(), default);
        Assert.Equal("post-value", result);
    }

    [Fact]
    public async Task Put_ShouldReturn_Response()
    {
        var handler = BuildHandler<PutCommand>(HttpMethodType.Put, "\"put-value\"");
        var result = await handler.HandleAsync(new PutCommand(), default);
        Assert.Equal("put-value", result);
    }

    [Fact]
    public async Task Delete_ShouldReturn_Response()
    {
        var handler = BuildHandler<DeleteCommand>(HttpMethodType.Delete, "\"delete-value\"");
        var result = await handler.HandleAsync(new DeleteCommand(), default);
        Assert.Equal("delete-value", result);
    }

    [Fact]
    public async Task PostCreate_ShouldReturn_Response()
    {
        var handler = BuildHandler<PostCreateCommand>(HttpMethodType.PostCreate, "\"create-value\"");
        var result = await handler.HandleAsync(new PostCreateCommand(), default);
        Assert.Equal("create-value", result);
    }

    [Fact]
    public async Task ShouldThrow_HttpRequestException_WhenStatusCodeIsError()
    {
        var handler = BuildHandler<DeleteCommand>(
            HttpMethodType.Delete,
            null,
            throwError: true);

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() =>
            handler.HandleAsync(new DeleteCommand(), CancellationToken.None).AsTask());

        Assert.Equal("Bad", exception.Message);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }
    
    [AutoApi(httpMethodType: HttpMethodType.Auto)]
    private class UnsupportedCommand : IRequest<string> { }
}