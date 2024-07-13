using Bogus;
using Kathanika.Infrastructure.Persistence.FileStorage;
using Kathanika.Web.FileOpsConfigurations;

namespace Kathanika.Web.Test.FileOpsConfigurations;

public class ApplicationFileIdProviderTests
{
    private readonly IFileMetadataService fileMetadataService = Substitute.For<IFileMetadataService>();

    [Fact]
    public async Task CreateId_Should_CallFileMetadataServiceAndReturnFileId()
    {
        ApplicationFileIdProvider applicationFileIdProvider = new(fileMetadataService);
        fileMetadataService.CreateAsync(Arg.Any<string>(), Arg.Any<string>())
            .Returns(Guid.NewGuid().ToString());

        string fileId = await applicationFileIdProvider.CreateId(new Faker().Lorem.Paragraph());

        Assert.NotNull(fileId);
    }

    [Fact]
    public async Task ValidateId_Should_CallFileMetadataServiceAndReturnResult()
    {
        bool existResult = new Faker().Random.Bool();
        ApplicationFileIdProvider applicationFileIdProvider = new(fileMetadataService);
        fileMetadataService.ExistAsync(Arg.Any<string>())
            .Returns(existResult);

        bool valid = await applicationFileIdProvider.ValidateId(Guid.NewGuid().ToString());

        Assert.Equal(existResult, valid);
    }
}
