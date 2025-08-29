namespace MitMediator.AutoApi.Abstractions;

public class FileRequest : IFileRequest
{
    public Stream File { get; private set; }
    
    public string FileName { get; private set; }
    
    public void SetFile(Stream file, string fileName)
    {
        File = file;
        FileName = fileName;
    }
    
    public async Task<byte[]> ReadToEndAsync(CancellationToken cancellationToken)
    {
        File.Seek(0, SeekOrigin.Begin);
        using var memoryStream = new MemoryStream();
        await File.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}