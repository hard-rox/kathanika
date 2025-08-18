using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibRecordAggregate;

public sealed class DataFieldTests
{
    [Theory]
    [InlineData("010", '0', '1')]
    [InlineData("020", ' ', ' ')]
    [InlineData("100", '1', '0')]
    [InlineData("245", '1', '0')]
    [InlineData("999", '9', '9')]
    public void Create_ShouldReturnSuccess_WhenValidParametersProvided(string tag, char indicator1, char indicator2)
    {
        // Arrange
        List<Subfield> subfields = [Subfield.Create('a', "Test Value").Value];

        // Act
        KnResult<DataField> result = DataField.Create(tag, indicator1, indicator2, subfields);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(tag, result.Value.Tag);
        Assert.Equal(indicator1, result.Value.Indicator1);
        Assert.Equal(indicator2, result.Value.Indicator2);
        Assert.Single(result.Value.Subfields);
    }

    [Theory]
    [InlineData("000")]
    [InlineData("001")]
    [InlineData("009")]
    [InlineData("00a")]
    [InlineData("a10")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("1234")]
    public void Create_ShouldReturnFailure_WhenTagIsInvalid(string invalidTag)
    {
        // Arrange
        List<Subfield> subfields = [Subfield.Create('a', "Test Value").Value];

        // Act
        KnResult<DataField> result = DataField.Create(invalidTag, ' ', ' ', subfields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "DataField.InvalidTag");
        Assert.Contains(result.Errors, e => e.Message.Contains(invalidTag));
    }

    [Theory]
    [InlineData('a')]
    [InlineData('Z')]
    [InlineData('!')]
    [InlineData('@')]
    public void Create_ShouldReturnFailure_WhenIndicator1IsInvalid(char invalidIndicator)
    {
        // Arrange
        List<Subfield> subfields = [Subfield.Create('a', "Test Value").Value];

        // Act
        KnResult<DataField> result = DataField.Create("100", invalidIndicator, ' ', subfields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "DataField.InvalidIndicator1");
        Assert.Contains(result.Errors, e => e.Message.Contains(invalidIndicator.ToString()));
    }

    [Theory]
    [InlineData('a')]
    [InlineData('Z')]
    [InlineData('!')]
    [InlineData('@')]
    public void Create_ShouldReturnFailure_WhenIndicator2IsInvalid(char invalidIndicator)
    {
        // Arrange
        List<Subfield> subfields = [Subfield.Create('a', "Test Value").Value];

        // Act
        KnResult<DataField> result = DataField.Create("100", ' ', invalidIndicator, subfields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "DataField.InvalidIndicator2");
        Assert.Contains(result.Errors, e => e.Message.Contains(invalidIndicator.ToString()));
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenSubfieldsIsNull()
    {
        // Act
        KnResult<DataField> result = DataField.Create("100", ' ', ' ', null);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "DataField.NoSubfields");
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenSubfieldsIsEmpty()
    {
        // Arrange
        List<Subfield> emptySubfields = [];

        // Act
        KnResult<DataField> result = DataField.Create("100", ' ', ' ', emptySubfields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "DataField.NoSubfields");
    }

    [Fact]
    public void Create_ShouldCreateReadOnlySubfieldsList_WhenSuccessful()
    {
        // Arrange
        List<Subfield> subfields =
        [
            Subfield.Create('a', "First Value").Value,
            Subfield.Create('b', "Second Value").Value
        ];

        // Act
        KnResult<DataField> result = DataField.Create("100", '1', '0', subfields);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<IReadOnlyList<Subfield>>(result.Value.Subfields);
        Assert.Equal(2, result.Value.Subfields.Count);
    }

    [Fact]
    public void GetAtomicValues_ShouldReturnAllValues_WhenCalled()
    {
        // Arrange
        List<Subfield> subfields =
        [
            Subfield.Create('a', "First Value").Value,
            Subfield.Create('b', "Second Value").Value
        ];
        DataField dataField = DataField.Create("100", '1', '0', subfields).Value;

        // Act
        List<object?> atomicValues = dataField.GetAtomicValues().ToList();

        // Assert
        Assert.Equal(5, atomicValues.Count); // Tag + Indicator1 + Indicator2 + 2 subfields
        Assert.Equal("100", atomicValues[0]);
        Assert.Equal('1', atomicValues[1]);
        Assert.Equal('0', atomicValues[2]);
        Assert.Equal(subfields[0], atomicValues[3]);
        Assert.Equal(subfields[1], atomicValues[4]);
    }
}