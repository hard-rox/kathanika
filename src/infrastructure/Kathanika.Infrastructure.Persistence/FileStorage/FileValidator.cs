
using System.Text.RegularExpressions;

namespace Kathanika.Infrastructure.Persistence.FileStorage;

internal abstract class FileValidator(
    IFileMetadataService fileMetadataService
)
{
    public async Task<bool> ValidateAsync(
        string fileId,
        long permittedMinSizeInBytes = 0,
        long permittedMaxSizeInBytes = 0,
        string[]? permittedContentTypes = null,
        string[]? permittedExtensions = null,
        CancellationToken cancellationToken = default
    )
    {
        StoredFileMetadata? metadata = await fileMetadataService.GetAsync(fileId, cancellationToken);
        if (metadata is null) return false;

        if (metadata.SizeInBytes > permittedMaxSizeInBytes
            || metadata.SizeInBytes < permittedMinSizeInBytes)
            return false;

        if (permittedContentTypes is not null
            && !HasValidContentType(metadata.ContentType, permittedContentTypes))
            return false;

        if (permittedExtensions is not null
            && !HasValidExtension(Path.GetExtension(metadata.FileName), permittedExtensions))
            return false;

        return true;
    }

    private static bool HasValidExtension(string extension, string[] permittedExtensions)
    {
        if (permittedExtensions.Contains(extension)) return true;
        return false;
    }

    private static bool HasValidContentType(string contentType, string[] permittedContentTypes)
    {
        if (permittedContentTypes.Contains(contentType)) return true;

        foreach (string permittedContentType in permittedContentTypes)
        {
            string regexPattern = "^" + Regex.Escape(permittedContentType).Replace(@"\*", ".*") + "$";
            if (Regex.IsMatch(contentType, regexPattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}