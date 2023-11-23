namespace Kathanika.Application.Features.Publishers.Commands;

internal sealed class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidator(IPublisherRepository publisherRepository)
    {
        RuleFor(x => new { x.Id, x.Patch })
            .MustAsync(async (props, CancellationToken) =>
            {
                bool isDuplicate = await publisherRepository.ExistsAsync(x => x.Name == props.Patch.Name, CancellationToken);
                return !isDuplicate;
            })
            .WithName("Name")
            .WithMessage("Duplicate author information {PropertyName}. Consider updating publisher");
    }
}
