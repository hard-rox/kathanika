namespace Kathanika.UnitTests.DomainUnitTests;

public class PublisherAggregateTests
{
    [Fact]
    public void Create_Should_return_Publisher_On_Valid_Input()
    {
        //Arrange

        //Act
        var publisher = Publisher.Create(
            "Seba",
            "Description",
            "12345678"
            );

        //Assert
        Assert.Equal("Seba", publisher.Name);
    }

    [Fact]
    public void Update_should_return_Publisher_On_Valid_Input()
    {
        ////Arrange
        //var publisher = Publisher.Create(
        //    "Seba",
        //    "Description",
        //    "12345678"
        //    );
        ////Act
        //publisher.Update()
    }
}
