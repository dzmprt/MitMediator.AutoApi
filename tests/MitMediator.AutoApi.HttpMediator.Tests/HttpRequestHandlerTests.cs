using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using Moq;
using Moq.Protected;

namespace MitMediator.AutoApi.HttpMediator.Tests;

public class GetFileWithNameQuery : IRequest<FileResponse>
{
    
}

public class GetFileQuery : IRequest<byte[]>
{
    
}

public class GetCommand : IRequest<string>
{
}

public class PostCommand : IRequest<string>
{

}

public class PutCommand : IRequest<string>
{

}

public class DeleteCommand : IRequest<string>
{

}

public class PostCreateCommand : IRequest<string>
{
}

public class HttpRequestHandlerTests
{
    private static HttpRequestHandler<TReq, TResponse> BuildHandler<TReq, TResponse>(HttpMethodType method, TResponse? responseContent,
        Action<HttpRequestMessage>? capture = null, bool customPattern = false, bool throwError = false, string? errorMessage = null)
        where TReq : IRequest<TResponse>
    {
        var msgHandlerMock = new Mock<HttpMessageHandler>();
        msgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                if (throwError)
                {
                    return errorMessage == null ? 
                        new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Bad" } : 
                        new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Bad", Content = new StringContent(errorMessage) };
                }

                if (typeof(TResponse) == typeof(string))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseContent?.ToString() ?? string.Empty) };
                }

                if (typeof(TResponse) == typeof(FileResponse))
                {
                    var resp = responseContent as FileResponse;
                    var context = new StreamContent(new MemoryStream(resp.Data));
                    var fileResp = new HttpResponseMessage(HttpStatusCode.OK) { Content = context };
                    fileResp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    fileResp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                        FileName = resp.FileName
                    };
                    return fileResp;
                }
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(responseContent as byte[]) };
            });

        var httpClient = new HttpClient(msgHandlerMock.Object)
        {
            BaseAddress = new Uri("https://host")
        };

        var clientFactory = new Mock<IHttpClientFactory>();
        clientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var headerMock = new Mock<IHttpHeaderInjector<TReq, TResponse>>();
        headerMock.Setup(h => h.GetHeadersAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(("X-Demo", "value"));

        var services = new ServiceCollection();
        services.AddSingleton(clientFactory.Object);
        services.AddSingleton<IEnumerable<IHttpHeaderInjector<TReq, TResponse>>>(new[] { headerMock.Object });

        return new HttpRequestHandler<TReq, TResponse>(services.BuildServiceProvider(), "https://base");
    }
    
    [Fact]
    public async Task Get_ShouldReturn_Response()
    {
        var handler = BuildHandler<GetCommand, string>(HttpMethodType.Get, "\"get-value\"");
        var result = await handler.HandleAsync(new GetCommand(), CancellationToken.None);
        Assert.Equal("get-value", result);
    }

    [Fact]
    public async Task Post_ShouldReturn_Response()
    {
        var handler = BuildHandler<PostCommand, string>(HttpMethodType.Post, "\"post-value\"");
        var result = await handler.HandleAsync(new PostCommand(), CancellationToken.None);
        Assert.Equal("post-value", result);
    }

    [Fact]
    public async Task Put_ShouldReturn_Response()
    {
        var handler = BuildHandler<PutCommand, string>(HttpMethodType.Put, "\"put-value\"");
        var result = await handler.HandleAsync(new PutCommand(), CancellationToken.None);
        Assert.Equal("put-value", result);
    }

    [Fact]
    public async Task Delete_ShouldReturn_Response()
    {
        var handler = BuildHandler<DeleteCommand, string>(HttpMethodType.Delete, "\"delete-value\"");
        var result = await handler.HandleAsync(new DeleteCommand(), CancellationToken.None);
        Assert.Equal("delete-value", result);
    }

    [Fact]
    public async Task PostCreate_ShouldReturn_Response()
    {
        var handler = BuildHandler<PostCreateCommand, string>(HttpMethodType.PostCreate, "\"create-value\"");
        var result = await handler.HandleAsync(new PostCreateCommand(), CancellationToken.None);
        Assert.Equal("create-value", result);
    }

    [Fact]
    public async Task ShouldThrow_HttpRequestException_WhenStatusCodeIsError()
    {
        var handler = BuildHandler<DeleteCommand, string>(
            HttpMethodType.Delete,
            null,
            throwError: true);

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() =>
            handler.HandleAsync(new DeleteCommand(), CancellationToken.None).AsTask());

        Assert.Equal("Bad", exception.Message);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }
    
    [Fact]
    public async Task ShouldThrow_HttpRequestException_WhenStatusCodeIsErrorAndBodyHaveErrorMessage()
    {
        var handler = BuildHandler<DeleteCommand, string>(
            HttpMethodType.Delete,
            null,
            throwError: true,
            errorMessage: "{error:\"error message\"}");

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() =>
            handler.HandleAsync(new DeleteCommand(), CancellationToken.None).AsTask());

        Assert.Equal("{error:\"error message\"}", exception.Message);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }
    
    [Fact]
    public async Task GetFileQuery_ShouldReturn_Response()
    {
        var handler = BuildHandler<GetFileQuery, byte[]>(HttpMethodType.PostCreate, "test"u8.ToArray());
        var result = await handler.HandleAsync(new GetFileQuery(),CancellationToken.None);
        Assert.Equal("test", Encoding.UTF8.GetString(result));
    }
    
    [Fact]
    public async Task TaskGetFileWithNameQuery_ShouldReturn_Response()
    {
        var handler = BuildHandler<GetFileWithNameQuery, FileResponse>(HttpMethodType.PostCreate, new FileResponse("test"u8.ToArray(), "testfile.txt"));
        var result = await handler.HandleAsync(new GetFileWithNameQuery(),CancellationToken.None);
        Assert.Equal("test", Encoding.UTF8.GetString(result.Data));
        Assert.Equal("testfile.txt", result.FileName);
    }
}