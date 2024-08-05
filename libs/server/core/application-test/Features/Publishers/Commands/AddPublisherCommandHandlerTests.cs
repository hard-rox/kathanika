using Kathanika.Core.Application.Features.Publishers.Commands;

namespace Kathanika.Core.Application.Test.Features.Publishers.Commands;

public class AddPublisherCommandHandlerTests
{
    private readonly IPublisherRepository repository;

    public AddPublisherCommandHandlerTests()
    {
        repository = Substitute.For<IPublisherRepository>();
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccessResult_OnValidInput()
    {
        Publisher dummy = new Faker<Publisher>()
            .CustomInstantiator(f => Publisher.Create(
                f.Company.CompanyName(),
                f.Lorem.Paragraph(),
                f.Address.FullAddress()
            ).Value);
        repository.AddAsync(Arg.Any<Publisher>(), Arg.Any<CancellationToken>())
            .Returns(dummy);
        AddPublisherCommand command = new(dummy.Name, dummy.Description, dummy.ContactInformation);
        AddPublisherCommandHandler handler = new(repository);

        Result<Publisher> result = await handler.Handle(command, default);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(dummy.Name, result.Value.Name);
        await repository
            .Received(1)
            .AddAsync(Arg.Any<Publisher>(), Arg.Any<CancellationToken>());
    }
}
