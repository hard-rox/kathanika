using Kathanika.Domain.Aggregates;

namespace Kathanika.Domain.Test;

public class PublisherAggregateTests
{
    [Fact]
    public void Create_Should_return_Publisher_On_Valid_Input()
    {
        //Arrange

        //Act
        Publisher publisher = Publisher.Create(
            "Sheba",
            "Description",
            "12345678"
            );

        //Assert
        Assert.Equal("Sheba", publisher.Name);
    }
}
