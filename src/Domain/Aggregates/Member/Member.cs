using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.Member;

public sealed class Member : AggregateRoot
{
    private readonly List<IssuedPublication> _currentlyIssuedPublications = new();

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public string Address { get; private set; }
    public string ContactNumber { get; private set; }
    public string Email { get; private set; }
    public DateTimeOffset MembershipStartDate { get; private init; } = DateTimeOffset.Now;
    public DateTimeOffset? MembershipCancellationDate { get; private set; }
    public DateTimeOffset? LastMembershipSuspensionDate { get; private set; }
    public MembershipStatus Status { get; private set; } = MembershipStatus.Active;

    private Member(
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        string address,
        string contactNumber,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Address = address;
        ContactNumber = contactNumber;
        Email = email;
    }

    public static Member Create(
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        string address,
        string contactNumber,
        string email
    )
    {
        return new Member(
            firstName,
            lastName,
            dateOfBirth,
            address,
            contactNumber,
            email
        );
    }

    public void Update(
    string? firstName,
    string? lastName,
    DateOnly? dateOfBirth,
    string? address,
    string? contactNumber,
    string? email)
    {
        FirstName = !string.IsNullOrEmpty(firstName) ? firstName : FirstName;
        LastName = !string.IsNullOrEmpty(lastName) ? lastName : LastName;
        DateOfBirth = dateOfBirth is not null ? (DateOnly)dateOfBirth : DateOfBirth;
        Address = !string.IsNullOrEmpty(address) ? address : Address;
        ContactNumber = !string.IsNullOrEmpty(contactNumber) ? contactNumber : ContactNumber;
        Email = !string.IsNullOrEmpty(email) ? email : Email;
    }

    public void CancelMembership()
    {
        if (_currentlyIssuedPublications.Any())
        {
            throw new MemberHasIssuedPublicationsException(_currentlyIssuedPublications.ToArray());
        }
        Status = MembershipStatus.Cancelled;
        MembershipCancellationDate = DateTimeOffset.Now;
    }

    public void SuspendMembership()
    {
        if (_currentlyIssuedPublications.Any())
        {
            throw new MemberHasIssuedPublicationsException(_currentlyIssuedPublications.ToArray());
        }
        Status = MembershipStatus.Suspended;
        LastMembershipSuspensionDate = DateTimeOffset.Now;
    }

    public void RenewMembership()
    {
        Status = MembershipStatus.Active;
    }
}
