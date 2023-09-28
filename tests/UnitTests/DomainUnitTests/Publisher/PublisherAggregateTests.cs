namespace Kathanika.UnitTests.DomainUnitTests;

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

    [Fact]
    public void Update_should_return_Publisher_On_Valid_Input()
    {
        ////Arrange
        //var publisher = Publisher.Create(
        //    "Sheba",
        //    "Description",
        //    "12345678"
        //    );
        ////Act
        //publisher.Update()
    }
}
