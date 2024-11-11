using Kathanika.Infrastructure.Persistence.FileStorage;
using NSubstitute.ReturnsExtensions;

namespace Kathanika.Infrastructure.Persistence.Tests.FileStorage;

public sealed class FileValidatorTests
{
    private class ConcreteFileValidator(IFileMetadataService fileMetadataService)
    : FileValidator(fileMetadataService);
    private readonly IFileMetadataService _fileMetadataService = Substitute.For<IFileMetadataService>();

    [Fact]
    public async Task ValidateAsync_ShouldReturnTrue_WhenFileIsValid()
    {
        FileValidator validator = new ConcreteFileValidator(_fileMetadataService);
        StoredFileMetadata metadata = new("file", "text/plain", 1000);
        _fileMetadataService.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(metadata);

        bool validationResult = await validator.ValidateAsync(
            Guid.NewGuid().ToString(),
            500,
            2000
        );

        Assert.True(validationResult);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnFalse_WhenNoMetadataFound()
    {
        FileValidator validator = new ConcreteFileValidator(_fileMetadataService);
        _fileMetadataService.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ReturnsNull();

        bool validationResult = await validator.ValidateAsync(
            Guid.NewGuid().ToString(),
            500,
            2000
        );

        Assert.False(validationResult);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnFalse_WhenFileIsTooSmall()
    {
        FileValidator validator = new ConcreteFileValidator(_fileMetadataService);
        StoredFileMetadata metadata = new("file", "text/plain", 100);
        _fileMetadataService.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(metadata);

        bool validationResult = await validator.ValidateAsync(
            Guid.NewGuid().ToString(),
            500,
            2000
        );

        Assert.False(validationResult);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnFalse_WhenFileIsTooBig()
    {
        FileValidator validator = new ConcreteFileValidator(_fileMetadataService);
        StoredFileMetadata metadata = new("file", "text/plain", 10000);
        _fileMetadataService.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(metadata);

        bool validationResult = await validator.ValidateAsync(
            Guid.NewGuid().ToString(),
            500,
            2000
        );

        Assert.False(validationResult);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnFalse_WhenFileContentIsInvalid()
    {
        FileValidator validator = new ConcreteFileValidator(_fileMetadataService);
        StoredFileMetadata metadata = new("file", "text/plain", 100);
        _fileMetadataService.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(metadata);

        bool validationResult = await validator.ValidateAsync(
            Guid.NewGuid().ToString(),
            500,
            2000,
            ["image/png"]
        );

        Assert.False(validationResult);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnFalse_WhenFileExtensionIsInvalid()
    {
        FileValidator validator = new ConcreteFileValidator(_fileMetadataService);
        StoredFileMetadata metadata = new("file", "text/plain", 100);
        _fileMetadataService.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(metadata);

        bool validationResult = await validator.ValidateAsync(
            Guid.NewGuid().ToString(),
            500,
            2000,
            permittedExtensions: [".txt"]
        );

        Assert.False(validationResult);
    }

    [Fact]
    public async Task ValidateAsync_ShouldSkipContentTypeAndExtensionCheck_WhenPermittedContentTypesAndExtensionsNotProvided()
    {
        FileValidator validator = new ConcreteFileValidator(_fileMetadataService);
        StoredFileMetadata metadata = new("file", "text/plain", 100);
        _fileMetadataService.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(metadata);

        bool validationResult = await validator.ValidateAsync(
            Guid.NewGuid().ToString(),
            100,
            2000
        );

        Assert.True(validationResult);
    }
}