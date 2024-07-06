using Kathanika.Core.Domain.DomainEvents;
using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.MemberAggregate;

public sealed class Member : AggregateRoot
{
    private readonly List<IssuedPublication> _currentlyIssuedPublications = [];

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PhotoFileId { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public string Address { get; private set; }
    public string ContactNumber { get; private set; }
    public string Email { get; private set; }
    public DateTimeOffset MembershipStartDateTime { get; private init; } = DateTimeOffset.Now;
    public DateTimeOffset? MembershipCancellationDateTime { get; private set; }
    public DateTimeOffset? LastMembershipSuspensionDateTime { get; private set; }
    public MembershipStatus Status { get; private set; } = MembershipStatus.Active;

    public string FullName => $"{FirstName} {LastName}";

    private Member(
        string firstName,
        string lastName,
        string photoFileId,
        DateOnly dateOfBirth,
        string address,
        string contactNumber,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        PhotoFileId = photoFileId;
        DateOfBirth = dateOfBirth;
        Address = address;
        ContactNumber = contactNumber;
        Email = email;
    }

    public static Member Create(
        string firstName,
        string lastName,
        string photoFileId,
        DateOnly dateOfBirth,
        string address,
        string contactNumber,
        string email
    )
    {
        Member newMember = new Member(
            firstName,
            lastName,
            photoFileId,
            dateOfBirth,
            address,
            contactNumber,
            email
        );

        newMember.AddDomainEvent(new FileUsedDomainEvent(photoFileId));

        return newMember;
    }

    public void Update(
    string? firstName,
    string? lastName,
    string? photoFileId,
    DateOnly? dateOfBirth,
    string? address,
    string? contactNumber,
    string? email)
    {
        FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : FirstName;
        LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : LastName;
        DateOfBirth = dateOfBirth is not null ? (DateOnly)dateOfBirth : DateOfBirth;
        Address = !string.IsNullOrWhiteSpace(address) ? address : Address;
        ContactNumber = !string.IsNullOrWhiteSpace(contactNumber) ? contactNumber : ContactNumber;
        Email = !string.IsNullOrWhiteSpace(email) ? email : Email;

        if (!string.IsNullOrWhiteSpace(photoFileId))
        {
            AddDomainEvent(new FileReplacedDomainEvent(PhotoFileId, photoFileId));
            PhotoFileId = photoFileId;
        }
    }

    public void CancelMembership()
    {
        if (_currentlyIssuedPublications.Count != 0)
        {
            throw new MemberHasIssuedPublicationsException(_currentlyIssuedPublications.ToArray());
        }
        Status = MembershipStatus.Cancelled;
        MembershipCancellationDateTime = DateTimeOffset.Now;
    }

    public void SuspendMembership()
    {
        if (_currentlyIssuedPublications.Count != 0)
        {
            throw new MemberHasIssuedPublicationsException(_currentlyIssuedPublications.ToArray());
        }
        Status = MembershipStatus.Suspended;
        LastMembershipSuspensionDateTime = DateTimeOffset.Now;
    }

    public void RenewMembership()
    {
        Status = MembershipStatus.Active;
    }
}
