using BooksCatalog.Domain.Abstractions;

namespace BooksCatalog.Application.Services;

public class ImagesService : IImagesService
{
    public bool IsPngImage(byte[] file)
    {
        // PNG signature: 89 50 4E 47 0D 0A 1A 0A
        byte[] pngSignature = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];

        if (file.Length < pngSignature.Length)
            return false;

        for (var i = 0; i < pngSignature.Length; i++)
        {
            if (file[i] != pngSignature[i])
            {
                return false;
            }
        }

        return true;
    }
}