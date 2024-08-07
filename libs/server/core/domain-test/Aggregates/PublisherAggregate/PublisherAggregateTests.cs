namespace Kathanika.Core.Domain.Test.Aggregates.PublisherAggregate;

public class PublisherAggregateTests
{
    [Fact]
    public void Create_Should_return_ResultOfPublisher_On_Valid_Input()
    {
        //Arrange

        //Act
        Result<Publisher> result = Publisher.Create(
            "Sheba",
            "Description",
            "12345678"
            );

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Sheba", result.Value.Name);
    }

    [Fact]
    public void Update_ShouldReturnUpdatedUpdated_WhenValidInput()
    {
        // Arrange
        Publisher publisher = Publisher.Create(
            "Sheba",
            "Description",
            "12345678"
            ).Value;

        string updatedName = "Updated Name";

        // Act
        Result result = publisher.Update(updatedName);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(updatedName, publisher.Name);
    }
}
