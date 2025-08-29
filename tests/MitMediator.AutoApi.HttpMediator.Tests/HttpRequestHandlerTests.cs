using System.IO.Pipelines;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using Moq;
using Moq.Protected;

namespace MitMediator.AutoApi.HttpMediator.Tests;

public class GetListQuery : IRequest<GetListResponse>
{
}

public class GetListResponse : ITotalCount
{
    public string[] Items { get; init; }

    internal int _totalCount;

    public int GetTotalCount() => _totalCount;

    public void SetTotalCount(int totalCount)
    {
        _totalCount = totalCount;
    }
}

public class GetFileStreamQuery : IRequest<Stream>
{
}

public class GetFileWithNameQuery : IRequest<FileResponse>
{
}

public class GetFileStreamWithNameQuery : IRequest<FileStreamResponse>
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
    private static HttpRequestHandler<TReq, TResponse> BuildHandler<TReq, TResponse>(HttpMethodType method,
        TResponse? responseContent,
        Action<HttpRequestMessage>? capture = null, bool customPattern = false, bool throwError = false,
        string? errorMessage = null)
        where TReq : IRequest<TResponse>
    {
        var msgHandlerMock = new Mock<HttpMessageHandler>();
        msgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                if (throwError)
                {
                    return errorMessage == null
                        ? new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Bad" }
                        : new HttpResponseMessage(HttpStatusCode.BadRequest)
                            { ReasonPhrase = "Bad", Content = new StringContent(errorMessage) };
                }

                if (typeof(TResponse) == typeof(string))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                        { Content = new StringContent(responseContent?.ToString() ?? string.Empty) };
                }
                
                if (typeof(TResponse) == typeof(Stream))
                {
                    var resp = responseContent as Stream;
                    var context = new StreamContent(resp);
                    var fileResp = new HttpResponseMessage(HttpStatusCode.OK) { Content = context };
                    fileResp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    return fileResp;
                }

                if (typeof(TResponse) == typeof(FileStreamResponse))
                {
                    var resp = responseContent as FileStreamResponse;
                    var context = new StreamContent(resp.File);
                    var fileResp = new HttpResponseMessage(HttpStatusCode.OK) { Content = context };
                    fileResp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    fileResp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = resp.FileName
                    };
                    return fileResp;
                }
                
                if (typeof(TResponse) == typeof(FileResponse))
                {
                    var resp = responseContent as FileResponse;
                    var context = new ByteArrayContent(resp.File);
                    var fileResp = new HttpResponseMessage(HttpStatusCode.OK) { Content = context };
                    fileResp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    fileResp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = resp.FileName
                    };
                    return fileResp;
                }

                if (responseContent is ITotalCount)
                {
                    var jsonResponse = JsonSerializer.Serialize(responseContent);
                    var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                        { Content = new StringContent(jsonResponse) };
                    var totalCount = responseContent as ITotalCount;
                    httpResponseMessage.Headers.Add("X-Total-Count", totalCount.GetTotalCount().ToString());
                    return httpResponseMessage;
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new ByteArrayContent(responseContent as byte[]) };
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
        var result = await handler.HandleAsync(new GetFileQuery(), CancellationToken.None);
        Assert.Equal("test", Encoding.UTF8.GetString(result));
    }
    
    [Fact]
    public async Task GetFileWithNameQuery_ShouldReturn_Response()
    {
        var handler = BuildHandler<GetFileWithNameQuery,
            FileResponse>(HttpMethodType.PostCreate,
            new FileResponse("test"u8.ToArray(), "testfile.txt"));
        var result = await handler.HandleAsync(new GetFileWithNameQuery(), CancellationToken.None);
        Assert.Equal("test", Encoding.UTF8.GetString(result.File));
        Assert.Equal("testfile.txt", result.FileName);
    }

    [Fact]
    public async Task GetFileStreamWithNameQuery_ShouldReturn_Response()
    {
       using var memoryStream = new MemoryStream("test"u8.ToArray());

        var handler = BuildHandler<GetFileStreamWithNameQuery,
            FileStreamResponse>(HttpMethodType.PostCreate,
            new FileStreamResponse(memoryStream, "testfile.txt"));
        var result = await handler.HandleAsync(new GetFileStreamWithNameQuery(), CancellationToken.None);
        memoryStream.Seek(0, SeekOrigin.Begin);
        Assert.Equal("test", Encoding.UTF8.GetString(await result.ReadToEndAsync(CancellationToken.None)));
        Assert.Equal("testfile.txt", result.FileName);
    }
    
    [Fact]
    public async Task GetFileStreamQuery_ShouldReturn_Response()
    {
        using var memoryStream = new MemoryStream("test"u8.ToArray());

        var handler = BuildHandler<GetFileStreamQuery, Stream>(HttpMethodType.PostCreate, memoryStream);
        var result = await handler.HandleAsync(new GetFileStreamQuery(), CancellationToken.None);
        memoryStream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(result, Encoding.UTF8, leaveOpen: true);
        var content = await reader.ReadToEndAsync();
        Assert.Equal("test", content);
    }

    [Fact]
    public async Task TaskGetListQuery_ShouldReturn_Response()
    {
        var response = new GetListResponse()
        {
            Items = Enumerable.Range(0, 3).Select(x => $"Record #{x}").ToArray()
        };
        response.SetTotalCount(100);
        var handler = BuildHandler<GetListQuery, GetListResponse>(HttpMethodType.Get, response);
        var result = await handler.HandleAsync(new GetListQuery(), CancellationToken.None);
        Assert.Equal(3, result.Items.Length);
        Assert.Equal(100, result.GetTotalCount());
        Assert.NotStrictEqual(result, response);
    }
}