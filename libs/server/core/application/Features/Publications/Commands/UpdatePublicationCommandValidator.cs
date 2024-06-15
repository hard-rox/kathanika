using System.Linq.Expressions;

namespace Kathanika.Core.Application.Features.Publications.Commands;

internal sealed class UpdatePublicationCommandValidator : AbstractValidator<UpdatePublicationCommand>
{
    public UpdatePublicationCommandValidator(IPublicationRepository publicationRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(publicationRepository.ExistsAsync)
            .WithMessage("Invalid publication");
        RuleFor(x => new { x.Id, x.Patch })
            .MustAsync(async (props, cancellationToken) =>
            {
                Expression<Func<Publication, bool>> expression = p =>
                    p.Id != props.Id
                    && p.Title == props.Patch.Title
                    && p.Isbn == props.Patch.Isbn
                    && p.PublicationType == props.Patch.PublicationType
                    && p.Edition == props.Patch.Edition;
                bool isDuplicate = await publicationRepository.ExistsAsync(expression, cancellationToken);
                return !isDuplicate;
            })
            .WithName("Title, ISBN, PublicationType, Edition")
            .WithMessage("Duplicate publication information {PropertyName}. Consider updating existing publication's 'Copies Available' field.");
    }
}
