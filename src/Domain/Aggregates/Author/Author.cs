using Kathanika.Domain.Exceptions;
using Kathanika.Domain.Premitives;

namespace Kathanika.Domain.Aggregates;

public sealed class Author : AggregateRoot
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; } = DateTime.MinValue;
    public DateTime? DateOfDeath { get; private set; }
    public string Nationality { get; private set; } = string.Empty;
    public string Biography { get; private set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public Author(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        DateTime? dateOfDeath,
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

    public void Update(
        string? firstName = null,
        string? lastName = null,
        DateTime? dateOfBirth = null,
        string? nationality = null,
        string? biography = null
    )
    {
        FirstName = !string.IsNullOrEmpty(firstName) ? firstName : FirstName;
        LastName = !string.IsNullOrEmpty(lastName) ? lastName : LastName;
        Nationality = !string.IsNullOrEmpty(nationality) ? nationality: Nationality;
        Biography = !string.IsNullOrEmpty(biography) ? biography: Biography;
        if(dateOfBirth is not null)
        {
            if (((DateTime)dateOfBirth).ToUniversalTime().Date > DateTime.UtcNow.Date)
                throw new InvalidFieldException(nameof(DateOfBirth), $"Cann't be future date");


            DateOfBirth = ((DateTime)dateOfBirth).Date;
        }
    }

    public void MakeAsDeceased(DateTime dateOfDeath)
    {
        if(dateOfDeath.ToUniversalTime().Date > DateTime.UtcNow.Date)
            throw new InvalidFieldException(nameof(dateOfDeath), $"Cann't be future date");

        if (dateOfDeath <= DateOfBirth)
            throw new InvalidFieldException(nameof(dateOfDeath), $"DateOfDeath must be after DateOfDeath");

        DateOfDeath = dateOfDeath.Date;
    }
}