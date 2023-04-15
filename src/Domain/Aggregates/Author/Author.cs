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
}