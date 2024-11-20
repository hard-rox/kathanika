using Kathanika.Domain.DomainEvents;
using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.PatronAggregate;

public sealed class Patron : AggregateRoot
{
    public string? Salutation { get; private set; }
    public string? FirstName { get; private set; }
    public string Surname { get; private set; }
    public string? PhotoFileId { get; private set; }
    public DateOnly? DateOfBirth { get; private set; }
    public string? Address { get; private set; }
    public string? ContactNumber { get; private set; }
    public string? Email { get; private set; }
    public string CardNumber { get; private set; }
    public DateOnly RegistrationDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

    public string FullName => $"{Salutation} {FirstName} {Surname}";

    private Patron(
        string surname,
        string cardNumber)
    {
        Surname = surname;
        CardNumber = cardNumber;
    }

    public static Result<Patron> Create(
        string surname,
        string cardNumber,
        string? salutation = null,
        string? firstName = null,
        string? photoFileId = null,
        DateOnly? dateOfBirth = null,
        string? address = null,
        string? contactNumber = null,
        string? email = null
    )
    {
        List<KnError> errors = [];
        if (dateOfBirth is not null && dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            errors.Add(PatronAggregateErrors.FutureDateOfBirth);
        }

        if (errors.Count != 0)
            return Result.Failure<Patron>(errors);

        Patron newPatron = new(
            surname,
            cardNumber
        )
        {
            Salutation = salutation,
            FirstName = firstName,
            PhotoFileId = photoFileId,
            DateOfBirth = dateOfBirth,
            Address = address,
            ContactNumber = contactNumber,
            Email = email
        };

        if (!string.IsNullOrWhiteSpace(photoFileId))
            newPatron.AddDomainEvent(new FileUsedDomainEvent(photoFileId));

        return Result.Success(newPatron);
    }

    public Result Update(
        string? cardNumber = null,
        string? salutation = null,
        string? firstName = null,
        string? surname = null,
        string? photoFileId = null,
        DateOnly? dateOfBirth = null,
        string? address = null,
        string? contactNumber = null,
        string? email = null)
    {
        CardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : CardNumber;
        Salutation = !string.IsNullOrWhiteSpace(salutation) ? salutation : Salutation;
        FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : FirstName;
        Surname = !string.IsNullOrWhiteSpace(surname) ? surname : Surname;
        DateOfBirth = dateOfBirth ?? DateOfBirth;
        Address = !string.IsNullOrWhiteSpace(address) ? address : Address;
        ContactNumber = !string.IsNullOrWhiteSpace(contactNumber) ? contactNumber : ContactNumber;
        Email = !string.IsNullOrWhiteSpace(email) ? email : Email;

        if (string.IsNullOrWhiteSpace(photoFileId))
            return Result.Success();
        
        IDomainEvent photoFileEvent
            = PhotoFileId is null
                ? new FileUsedDomainEvent(photoFileId)
                : new FileReplacedDomainEvent(PhotoFileId, photoFileId);

        PhotoFileId = photoFileId;
        AddDomainEvent(photoFileEvent);

        return Result.Success();
    }
}