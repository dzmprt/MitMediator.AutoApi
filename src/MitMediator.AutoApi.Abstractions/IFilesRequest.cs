namespace MitMediator.AutoApi.Abstractions;

public interface IFilesRequest
{
    IFileRequest[] Files { get; }

    void SetFiles(IFileRequest[] files);
}