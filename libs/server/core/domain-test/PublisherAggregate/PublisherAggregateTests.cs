namespace Kathanika.Core.Domain.Test.PublisherAggregate;

public class PublisherAggregateTests
{
    [Fact]
    public void Create_Should_return_ResultOfPublisher_On_Valid_Input()
    {
        //Arrange

        //Act
        Primitives.Result<Publisher> result = Publisher.Create(
            "Sheba",
            "Description",
            "12345678"
            );

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Sheba", result.Value.Name);
    }
}
