using System.Text;
using Kathanika.Infrastructure.Persistence.FileStorage;
using tusdotnet.Interfaces;
using tusdotnet.Models;

namespace Kathanika.Web.FileOpsConfigurations;

internal sealed class ApplicationFileIdProvider(
    IFileMetadataService fileMetadataService
) : ITusFileIdProvider
{
    public async Task<string> CreateId(string metadata)
    {
        Dictionary<string, Metadata> parsedMetadata = Metadata.Parse(metadata);
        string contentType = parsedMetadata.TryGetValue("filetype", out Metadata? contentTypeMetadata) ?
            contentTypeMetadata.GetString(Encoding.UTF8)
            : "application/octet-stream";

        string fileName = parsedMetadata.TryGetValue("filename", out Metadata? fileNameMetadata) ?
            fileNameMetadata.GetString(Encoding.UTF8)
            : string.Empty;

        string fileId = await fileMetadataService.CreateAsync(fileName, contentType);
        return fileId;
    }

    public async Task<bool> ValidateId(string fileId)
    {
        bool valid = await fileMetadataService.ExistAsync(fileId);
        return valid;
    }
}
