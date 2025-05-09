using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Domain.Tests.Aggregates.PatronAggregate;

public class PatronTests
{
    private readonly Faker<Patron> _patronFaker = new Faker<Patron>()
        .CustomInstantiator(f => Patron.Create(f.Person.LastName, f.Random.AlphaNumeric(10)).Value);

    [Fact]
    public void Create_ShouldReturnNewPatron_WithValidData()
    {
        // Arrange
        Patron dummyPatron = _patronFaker.Generate();
        // Act
        KnResult<Patron> patronResult = Patron.Create(dummyPatron.Surname, dummyPatron.CardNumber);
        Patron patron = patronResult.Value;

        // Assert
        Assert.NotNull(patron);
        Assert.Equal(dummyPatron.Surname, patron.Surname);
        Assert.Equal(dummyPatron.CardNumber, patron.CardNumber);
        Assert.Equal(dummyPatron.FirstName, patron.FirstName);
        Assert.Equal(dummyPatron.PhotoFileId, patron.PhotoFileId);
        Assert.Equal(dummyPatron.DateOfBirth, patron.DateOfBirth);
        Assert.Equal(dummyPatron.Address, patron.Address);
        Assert.Equal(dummyPatron.ContactNumber, patron.ContactNumber);
        Assert.Equal(dummyPatron.Email, patron.Email);
    }

    [Fact]
    public void Update_ShouldModifyExistingPatron_WithValidData()
    {
        // Arrange
        Patron existingPatron = _patronFaker.Generate();
        KnResult<Patron> initialResult = Patron.Create(existingPatron.Surname, existingPatron.CardNumber);
        Patron patronToUpdate = initialResult.Value;

        // New data for the update
        const string newFirstName = "UpdatedFirstName";
        const string newSurname = "UpdatedSurname";
        const string newCardNumber = "UpdatedCardNumber123";
        const string newAddress = "Updated Address";

        // Act
        KnResult updateKnResult = patronToUpdate.Update(newCardNumber, firstName: newFirstName, surname: newSurname,
            address: newAddress);

        // Assert
        Assert.True(updateKnResult.IsSuccess);
        Assert.NotNull(patronToUpdate);
        Assert.Equal(newFirstName, patronToUpdate.FirstName);
        Assert.Equal(newSurname, patronToUpdate.Surname);
        Assert.Equal(newCardNumber, patronToUpdate.CardNumber);
        Assert.Equal(newAddress, patronToUpdate.Address);
        Assert.Equal(existingPatron.PhotoFileId,
            patronToUpdate.PhotoFileId); // Assuming photo file ID should not change
        Assert.Equal(existingPatron.DateOfBirth,
            patronToUpdate.DateOfBirth); // Assuming date of birth should not change
        Assert.Equal(existingPatron.ContactNumber,
            patronToUpdate.ContactNumber); // Assuming contact number should not change
        Assert.Equal(existingPatron.Email, patronToUpdate.Email); // Assuming email should not change
    }
}