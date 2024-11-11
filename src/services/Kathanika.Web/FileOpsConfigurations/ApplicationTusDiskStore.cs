using Microsoft.Extensions.Options;
using tusdotnet.Interfaces;
using tusdotnet.Stores;

namespace Kathanika.Web.FileOpsConfigurations;

internal class ApplicationTusDiskStore(IOptions<ApplicationOptions> options, ITusFileIdProvider databaseFileIdProvider)
    : TusDiskStore(directoryPath: options.Value.UploadPath,
            deletePartialFilesOnConcat: true,
            bufferSize: TusDiskBufferSize.Default,
            fileIdProvider: databaseFileIdProvider), ITusStore;