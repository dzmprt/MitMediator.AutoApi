using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using Moq;

namespace MitMediator.AutoApi.Tests.EndpointsMethodsTests;

public class GetFileTests
{
    [Fact]
    public async Task GetFileQuery_BytesArrayResult_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        mediatorMock
            .Setup(m => m.SendAsync<RequestsForTests.Files.Queries.GetFile.GetFileQuery, byte[]>(
                It.IsAny<RequestsForTests.Files.Queries.GetFile.GetFileQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync("test"u8.ToArray());

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .WithGetParams<RequestsForTests.Files.Queries.GetFile.GetFileQuery, byte[]>(new RequestInfo(typeof(RequestsForTests.Files.Queries.GetFile.GetFileQuery)));
        var result = await (ValueTask<IResult>)del.DynamicInvoke(context, CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileContentHttpResult>(result);
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }
    
    [Fact]
    public async Task GetFileWithCustomNameQuery_FileResponseResult_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        mediatorMock
            .Setup(m => m.SendAsync<RequestsForTests.Files.Queries.GetFileWithCustomName.GetFileWithCustomNameQuery, FileResponse>(
                It.IsAny<RequestsForTests.Files.Queries.GetFileWithCustomName.GetFileWithCustomNameQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FileResponse("test"u8.ToArray(), "test.txt"));

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .WithGetParams<RequestsForTests.Files.Queries.GetFileWithCustomName.GetFileWithCustomNameQuery, FileResponse>(new RequestInfo(typeof(RequestsForTests.Files.Queries.GetFileWithCustomName.GetFileWithCustomNameQuery)));
        var result = await (ValueTask<IResult>)del.DynamicInvoke(context, CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileContentHttpResult>(result);
        Assert.Equal("test.txt", GetFileName(context.Response));
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }
    
    [Fact]
    public async Task GetFileStreamQuery_StreamResult_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();
        
        var context = new DefaultHttpContext { RequestServices = services };
        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;
        
        mediatorMock
            .Setup(m => m.SendAsync<RequestsForTests.Files.Queries.GetFileStream.GetFileStreamQuery, Stream>(
                It.IsAny<RequestsForTests.Files.Queries.GetFileStream.GetFileStreamQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MemoryStream("test"u8.ToArray()));
        
        var del = EndpointsMethods
            .WithGetParams<RequestsForTests.Files.Queries.GetFileStream.GetFileStreamQuery, Stream>(new RequestInfo(typeof(RequestsForTests.Files.Queries.GetFileStream.GetFileStreamQuery)));
        var result = await (ValueTask<IResult>)del.DynamicInvoke(context, CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileStreamHttpResult>(result);
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }
    
    [Fact]
    public async Task GetFileStreamWithCustomName_FileResponseResult_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        mediatorMock
            .Setup(m => m.SendAsync<RequestsForTests.Files.Queries.GetFileStreamWithCustomName.GetFileStreamWithCustomNameQuery, FileStreamResponse>(
                It.IsAny<RequestsForTests.Files.Queries.GetFileStreamWithCustomName.GetFileStreamWithCustomNameQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FileStreamResponse(new MemoryStream("test"u8.ToArray()), "test.txt"));

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .WithGetParams<RequestsForTests.Files.Queries.GetFileStreamWithCustomName.GetFileStreamWithCustomNameQuery, FileStreamResponse>(new RequestInfo(typeof(RequestsForTests.Files.Queries.GetFileStreamWithCustomName.GetFileStreamWithCustomNameQuery)));
        var result = await (ValueTask<IResult>)del.DynamicInvoke(context, CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileStreamHttpResult>(result);
        Assert.Equal("test.txt", GetFileName(context.Response));
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }
    
    private static string? GetFileName(HttpResponse response)
    {
        if (response.Headers.TryGetValue("Content-Disposition", out var contentDisposition))
        {
            var disposition = contentDisposition.ToString();
            var filenamePart = disposition.Split(';')
                .FirstOrDefault(p => p.Trim().StartsWith("filename=", StringComparison.OrdinalIgnoreCase));

            if (filenamePart != null)
            {
                var filename = filenamePart.Split('=')[1].Trim().Trim('"');
                return filename;
            }
        }

        return null;
    }
}