namespace Kathanika.Application.Features.Publishers.Commands;

internal sealed class AddPublisherCommandValidator : AbstractValidator<AddPublisherCommand>
{
    public AddPublisherCommandValidator(IPublisherRepository publisherRepository)
    {
        RuleFor(x => x.Name.Trim())
            .NotEmpty();

        RuleFor(x => x.Name)
            .MustAsync(async (name, CancellationToken) =>
            {
                bool isDuplicate = await publisherRepository.ExistsAsync(x => x.Name == name, CancellationToken);
                return !isDuplicate;
            })
            .WithMessage("Duplication publisher name");
    }
}
