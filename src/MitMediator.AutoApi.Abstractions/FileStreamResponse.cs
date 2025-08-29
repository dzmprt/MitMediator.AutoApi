namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// File steam response with custom file name.
/// </summary>
public sealed class FileStreamResponse(Stream file, string fileName)
{
    public string FileName { get; } = fileName;
    public Stream File { get; } = file;
    
    
    public async Task<byte[]> ReadToEndAsync(CancellationToken cancellationToken)
    {
        File.Seek(0, SeekOrigin.Begin);
        using var memoryStream = new MemoryStream();
        await File.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}