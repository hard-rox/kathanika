using Kathanika.Domain.DomainEvents;
using Kathanika.Domain.Exceptions;
using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class Author : AggregateRoot
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateOnly DateOfBirth { get; private set; } = DateOnly.MinValue;
    public DateOnly? DateOfDeath { get; private set; }
    public string Nationality { get; private set; } = string.Empty;
    public string Biography { get; private set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    private Author(
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        DateOnly? dateOfDeath,
        string nationality,
        string biography
    )
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        DateOfDeath = dateOfDeath;
        Nationality = nationality;
        Biography = biography;
    }

    public static Author Create(
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        DateOnly? dateOfDeath,
        string nationality,
        string biography
    )
    {
        List<DomainException> errors = new List<DomainException>();
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

        return new Author(
            firstName,
            lastName,
            dateOfBirth,
            dateOfDeath,
            nationality,
            biography);
    }

    public void Update(
        string? firstName = null,
        string? lastName = null,
        DateOnly? dateOfBirth = null,
        string? nationality = null,
        string? biography = null
    )
    {
        FirstName = !string.IsNullOrEmpty(firstName) ? firstName : FirstName;
        LastName = !string.IsNullOrEmpty(lastName) ? lastName : LastName;
        Nationality = !string.IsNullOrEmpty(nationality) ? nationality : Nationality;
        Biography = !string.IsNullOrEmpty(biography) ? biography : Biography;
        if (dateOfBirth is not null)
        {
            if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidFieldException(nameof(DateOfBirth), $"Cann't be future date");

            DateOfBirth = (DateOnly)dateOfBirth;
        }

        AddDomainEvent(new AuthorUpdatedDomainEvent(Id));
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
