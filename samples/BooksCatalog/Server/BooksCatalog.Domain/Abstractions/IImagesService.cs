namespace BooksCatalog.Domain.Abstractions;

/// <summary>
/// Images service.
/// </summary>
public interface IImagesService
{
    /// <summary>
    /// Checks whether the file is in PNG format.
    /// </summary>
    /// <param name="file">The file content as a byte array.</param>
    /// <returns><c>True</c> if the file is a PNG image; otherwise, <c>false</c>.</returns>
    bool IsPngImage(byte[] file);
}