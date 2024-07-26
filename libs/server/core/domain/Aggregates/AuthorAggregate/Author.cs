using Kathanika.Core.Domain.DomainEvents;
using Kathanika.Core.Domain.Exceptions;
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

    public static Author Create(
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        DateOnly? dateOfDeath,
        string nationality,
        string biography,
        string? dpFileId = null
    )
    {
        List<DomainException> errors = [];
        if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            errors.Add(new InvalidFieldException(nameof(DateOfBirth), $"Cann't be future date"));
        }
        if (dateOfDeath > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            errors.Add(new InvalidFieldException(nameof(DateOfDeath), $"Cann't be future date"));
        }
        if (dateOfDeath is not null && dateOfDeath <= dateOfBirth)
        {
            errors.Add(new InvalidFieldException(nameof(DateOfDeath), $"{nameof(DateOfDeath)} must be after {nameof(DateOfBirth)}"));
        }

        if (errors.Count > 0)
        {
            throw new AggregateException(errors);
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

        return author;
    }

    public void Update(
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
                throw new InvalidFieldException(nameof(DateOfBirth), $"Cann't be future date");

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
    }

    public void MarkAsDeceased(DateOnly dateOfDeath)
    {
        if (dateOfDeath > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new InvalidFieldException(nameof(DateOfDeath), $"Cann't be future date");

        if (dateOfDeath <= DateOfBirth)
            throw new InvalidFieldException(nameof(DateOfDeath), $"{nameof(DateOfDeath)} must be after {nameof(DateOfBirth)}");

        DateOfDeath = dateOfDeath;
    }

    public void UnmarkAsDeceased()
    {
        DateOfDeath = null;
    }
}