namespace MitMediator.AutoApi.Abstractions.Tests;

using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class FileRequestTests
{
    [Fact]
    public async Task ReadToEndAsync_ReturnsCorrectBytes()
    {
        // Arrange
        var content = "Hello!";
        var contentBytes = Encoding.UTF8.GetBytes(content);
        using var inputStream = new MemoryStream(contentBytes);

        var fileRequest = new FileRequest();
        fileRequest.SetFile(inputStream, "greeting.txt");

        // Act
        var result = await fileRequest.ReadToEndAsync(CancellationToken.None);

        // Assert
        Assert.Equal(contentBytes, result);
        Assert.Equal("greeting.txt", fileRequest.GetFileName());
        Assert.NotNull(fileRequest.GetFileName());
        Assert.True(fileRequest.GetFileStream().CanRead);
    }

    [Fact]
    public async Task ReadToEndAsync_ResetsStreamPosition()
    {
        // Arrange
        var content = "Stream position test";
        var contentBytes = Encoding.UTF8.GetBytes(content);
        using var inputStream = new MemoryStream(contentBytes);

        var fileRequest = new FileRequest();
        fileRequest.SetFile(inputStream, "position.txt");
        inputStream.Seek(5, SeekOrigin.Begin);

        // Act
        var result = await fileRequest.ReadToEndAsync(CancellationToken.None);

        // Assert
        Assert.Equal(contentBytes, result); // Should read from beginning
    }
}