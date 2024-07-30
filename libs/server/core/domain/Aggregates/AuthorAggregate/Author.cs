using Kathanika.Core.Domain.DomainEvents;
using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.AuthorAggregate;

public sealed class Author : AggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public DateOnly? DateOfDeath { get; private set; }
    public string Nationality { get; private set; }
    public string Biography { get; private set; }
    public string? DpFileId { get; private set; } = null;

    public string FullName => $"{FirstName} {LastName}";

    private Author(
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        DateOnly? dateOfDeath,
        string nationality,
        string biography,
        string? dpFileId = null
    )
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        DateOfDeath = dateOfDeath;
        Nationality = nationality;
        Biography = biography;
        DpFileId = dpFileId;
    }

    public static Result<Author> Create(
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        DateOnly? dateOfDeath,
        string nationality,
        string biography,
        string? dpFileId = null
    )
    {
        List<KnError> errors = [];
        if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            errors.Add(AuthorAggregateErrors.FutureDateOfBirth);
        }
        if (dateOfDeath > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            errors.Add(AuthorAggregateErrors.FutureDateOfDeath);
        }
        if (dateOfDeath is not null && dateOfDeath <= dateOfBirth)
        {
            errors.Add(AuthorAggregateErrors.DateOfBirthFollowingDateOfDeath);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<Author>(errors);
        }

        Author author = new(
            firstName,
            lastName,
            dateOfBirth,
            dateOfDeath,
            nationality,
            biography,
            dpFileId);

        if (!string.IsNullOrWhiteSpace(dpFileId))
            author.AddDomainEvent(new FileUsedDomainEvent(dpFileId));

        return Result.Success(author);
    }

    public Result Update(
        string? firstName = null,
        string? lastName = null,
        DateOnly? dateOfBirth = null,
        string? nationality = null,
        string? biography = null,
        string? dpFileId = null
    )
    {
        FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : FirstName;
        LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : LastName;
        Nationality = !string.IsNullOrWhiteSpace(nationality) ? nationality : Nationality;
        Biography = !string.IsNullOrWhiteSpace(biography) ? biography : Biography;
        if (dateOfBirth is not null)
        {
            if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
                return AuthorAggregateErrors.FutureDateOfBirth;

            DateOfBirth = (DateOnly)dateOfBirth;
        }

        AddDomainEvent(new AuthorUpdatedDomainEvent(Id));

        if (!string.IsNullOrWhiteSpace(dpFileId?.Trim()))
        {
            if (!string.IsNullOrWhiteSpace(DpFileId))
                AddDomainEvent(new FileReplacedDomainEvent(DpFileId, dpFileId));
            else AddDomainEvent(new FileUsedDomainEvent(dpFileId));
            DpFileId = dpFileId.Trim();
        }

        return Result.Success();
    }

    public Result MarkAsDeceased(DateOnly dateOfDeath)
    {
        if (dateOfDeath > DateOnly.FromDateTime(DateTime.UtcNow))
            return AuthorAggregateErrors.FutureDateOfDeath;

        if (dateOfDeath <= DateOfBirth)
            return AuthorAggregateErrors.DateOfBirthFollowingDateOfDeath;

        DateOfDeath = dateOfDeath;

        return Result.Success();
    }

    public Result UnmarkAsDeceased()
    {
        DateOfDeath = null;
        return Result.Success();
    }
}
