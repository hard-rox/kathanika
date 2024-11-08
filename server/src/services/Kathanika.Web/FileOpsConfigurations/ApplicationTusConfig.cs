using Kathanika.Infrastructure.Persistence.FileStorage;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;

namespace Kathanika.Web.FileOpsConfigurations;

public class ApplicationTusConfig : DefaultTusConfiguration
{
    public ApplicationTusConfig(ITusStore store, IFileMetadataService fileMetadataService)
    {
        Store = store;
        MaxAllowedUploadSizeInBytes = 30000000; //Default kestrel allowed size.
        Events = new Events()
        {
            OnFileCompleteAsync = async context =>
            {
                ITusFile file = await context.GetFileAsync();
                Stream content = await file.GetContentAsync(context.CancellationToken);
                long fileSize = content.Length;

                await fileMetadataService.RecordUploadCompletedAsync(file.Id, fileSize);
            }
        };
    }
}
