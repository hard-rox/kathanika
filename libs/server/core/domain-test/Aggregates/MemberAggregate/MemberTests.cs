using Kathanika.Core.Domain.Aggregates.MemberAggregate;

namespace Kathanika.Core.Domain.Test.Aggregates.MemberAggregate;

public class MemberTests
{
    private readonly Faker<Member> _memberFaker = new Faker<Member>()
            .CustomInstantiator(f => Member.Create(
                f.Name.FirstName(),
                f.Name.LastName(),
                f.Random.Guid().ToString(),
                DateOnly.FromDateTime(f.Date.Past(30, DateTime.Now.AddYears(-18))),
                f.Address.FullAddress(),
                f.Phone.PhoneNumber(),
                f.Internet.Email()).Value);

    [Fact]
    public void Create_ShouldReturnNewMember_WithValidData()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        string photoFileId = Guid.NewGuid().ToString();
        DateOnly dateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));
        string address = "123 Street";
        string contactNumber = "123456789";
        string email = "john.doe@example.com";

        // Act
        Result<Member> memberResult = Member.Create(firstName, lastName, photoFileId, dateOfBirth, address, contactNumber, email);
        Member member = memberResult.Value;

        // Assert
        Assert.NotNull(member);
        Assert.Equal(firstName, member.FirstName);
        Assert.Equal(lastName, member.LastName);
        Assert.Equal(photoFileId, member.PhotoFileId);
        Assert.Equal(dateOfBirth, member.DateOfBirth);
        Assert.Equal(address, member.Address);
        Assert.Equal(contactNumber, member.ContactNumber);
        Assert.Equal(email, member.Email);
        Assert.Equal(MembershipStatus.Active, member.Status);
    }

    [Fact]
    public void Update_ShouldUpdateMember_WithValidData()
    {
        // Arrange
        Member member = _memberFaker.Generate();
        string newFirstName = "Jane";
        string newLastName = "Smith";
        string newPhotoFileId = Guid.NewGuid().ToString();
        DateOnly newDateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-25));
        string newAddress = "456 Avenue";
        string newContactNumber = "987654321";
        string newEmail = "jane.smith@example.com";

        // Act
        Result result = member.Update(newFirstName, newLastName, newPhotoFileId, newDateOfBirth, newAddress, newContactNumber, newEmail);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newFirstName, member.FirstName);
        Assert.Equal(newLastName, member.LastName);
        Assert.Equal(newPhotoFileId, member.PhotoFileId);
        Assert.Equal(newDateOfBirth, member.DateOfBirth);
        Assert.Equal(newAddress, member.Address);
        Assert.Equal(newContactNumber, member.ContactNumber);
        Assert.Equal(newEmail, member.Email);
    }

    [Fact]
    public void CancelMembership_ShouldFail_WhenMemberHasIssuedPublications()
    {
        // Arrange
        Member member = _memberFaker.Generate();
        member.GetType()
            .GetField("_currentlyIssuedPublications", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
            .SetValue(member, new List<IssuedPublication>
            {
                new Faker<IssuedPublication>()
                    .CustomInstantiator(f => new IssuedPublication(
                        Guid.NewGuid().ToString(),
                        f.Lorem.Sentence(),
                        f.Random.Enum<PublicationType>(),
                        f.Random.AlphaNumeric(10)
                    ))
            });

        // Act
        Result result = member.CancelMembership();

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.HasIssuedPublication(1), result.Errors);
    }

    [Fact]
    public void CancelMembership_ShouldSucceed_WhenMemberHasNoIssuedPublications()
    {
        // Arrange
        Member member = _memberFaker.Generate();

        // Act
        Result result = member.CancelMembership();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(MembershipStatus.Cancelled, member.Status);
        Assert.NotNull(member.MembershipCancellationDateTime);
    }

    [Fact]
    public void SuspendMembership_ShouldFail_WhenMemberHasIssuedPublications()
    {
        // Arrange
        Member member = _memberFaker.Generate();
        member.GetType()
            .GetField("_currentlyIssuedPublications", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
            .SetValue(member, new List<IssuedPublication>
            {
                new Faker<IssuedPublication>()
                    .CustomInstantiator(f => new IssuedPublication(
                        Guid.NewGuid().ToString(),
                        f.Lorem.Sentence(),
                        f.Random.Enum<PublicationType>(),
                        f.Random.AlphaNumeric(10)
                    ))
            });
        // Act
        Result result = member.SuspendMembership();

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.HasIssuedPublication(1), result.Errors);
    }

    [Fact]
    public void SuspendMembership_ShouldSucceed_WhenMemberHasNoIssuedPublications()
    {
        // Arrange
        Member member = _memberFaker.Generate();

        // Act
        Result result = member.SuspendMembership();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(MembershipStatus.Suspended, member.Status);
        Assert.NotNull(member.LastMembershipSuspensionDateTime);
    }

    [Fact]
    public void RenewMembership_ShouldFail_WhenMembershipIsCancelled()
    {
        // Arrange
        Member member = _memberFaker.Generate();
        member.CancelMembership();

        // Act
        Result result = member.RenewMembership();

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.CancelledMembership, result.Errors);
    }

    [Fact]
    public void RenewMembership_ShouldFail_WhenMembershipIsActive()
    {
        // Arrange
        Member member = _memberFaker.Generate();

        // Act
        Result result = member.RenewMembership();

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.ActiveMembership, result.Errors);
    }

    [Fact]
    public void RenewMembership_ShouldSucceed_WhenMembershipIsSuspended()
    {
        // Arrange
        Member member = _memberFaker.Generate();
        member.SuspendMembership();

        // Act
        Result result = member.RenewMembership();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(MembershipStatus.Active, member.Status);
    }
}
