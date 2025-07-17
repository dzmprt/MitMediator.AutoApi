namespace MitMediator.AutoApi.Abstractions;

public sealed class FileResponse 
{
    public string FileName { get; }
    public byte[] Data { get; }

    public FileResponse(byte[] data, string fileName)
    {
        Data = data;
        FileName = fileName;
    }
}