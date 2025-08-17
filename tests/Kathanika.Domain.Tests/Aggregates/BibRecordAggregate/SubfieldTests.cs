using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibRecordAggregate;

public sealed class SubfieldTests
{
    [Theory]
    [InlineData('a', "Test Value")]
    [InlineData('z', "Another Value")]
    [InlineData('0', "Numeric Code")]
    [InlineData('9', "Another Numeric")]
    [InlineData('!', "Special Character")]
    [InlineData('$', "Dollar Sign")]
    [InlineData('%', "Percent Sign")]
    public void Create_ShouldReturnSuccess_WhenValidCodeAndValueProvided(char code, string value)
    {
        // Act
        KnResult<Subfield> result = Subfield.Create(code, value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(code, result.Value.Code);
        Assert.Equal(value, result.Value.Value);
    }

    [Theory]
    [InlineData('A')]
    [InlineData('Z')]
    [InlineData('@')]
    [InlineData('#')]
    [InlineData('&')]
    [InlineData('*')]
    [InlineData('+')]
    [InlineData('=')]
    [InlineData('[')]
    [InlineData(']')]
    public void Create_ShouldReturnFailure_WhenCodeIsInvalid(char invalidCode)
    {
        // Act
        KnResult<Subfield> result = Subfield.Create(invalidCode, "Test Value");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "Subfield.InvalidCode");
        Assert.Contains(result.Errors, e => e.Message.Contains(invalidCode.ToString()));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_ShouldReturnFailure_WhenValueIsNullOrEmpty(string? value)
    {
        // Act
        KnResult<Subfield> result = Subfield.Create('a', value);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "Subfield.EmptyValue");
    }

    [Fact]
    public void Create_ShouldReturnSuccess_WhenValueIsWhitespace()
    {
        // Act
        KnResult<Subfield> result = Subfield.Create('a', " ");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(" ", result.Value.Value);
    }

    [Fact]
    public void GetAtomicValues_ShouldReturnCodeAndValue_WhenCalled()
    {
        // Arrange
        var code = 'a';
        var value = "Test Value";
        Subfield subfield = Subfield.Create(code, value).Value;

        // Act
        List<object?> atomicValues = subfield.GetAtomicValues().ToList();

        // Assert
        Assert.Equal(2, atomicValues.Count);
        Assert.Equal(code, atomicValues[0]);
        Assert.Equal(value, atomicValues[1]);
    }

    [Fact]
    public void Equality_ShouldReturnTrue_WhenSubfieldsHaveSameCodeAndValue()
    {
        // Arrange
        Subfield subfield1 = Subfield.Create('a', "Test Value").Value;
        Subfield subfield2 = Subfield.Create('a', "Test Value").Value;

        // Act & Assert
        Assert.Equal(subfield1, subfield2);
        Assert.True(subfield1 == subfield2);
        Assert.False(subfield1 != subfield2);
    }

    [Fact]
    public void Equality_ShouldReturnFalse_WhenSubfieldsHaveDifferentCodes()
    {
        // Arrange
        Subfield subfield1 = Subfield.Create('a', "Test Value").Value;
        Subfield subfield2 = Subfield.Create('b', "Test Value").Value;

        // Act & Assert
        Assert.NotEqual(subfield1, subfield2);
        Assert.False(subfield1 == subfield2);
        Assert.True(subfield1 != subfield2);
    }

    [Fact]
    public void Equality_ShouldReturnFalse_WhenSubfieldsHaveDifferentValues()
    {
        // Arrange
        Subfield subfield1 = Subfield.Create('a', "First Value").Value;
        Subfield subfield2 = Subfield.Create('a', "Second Value").Value;

        // Act & Assert
        Assert.NotEqual(subfield1, subfield2);
        Assert.False(subfield1 == subfield2);
        Assert.True(subfield1 != subfield2);
    }

    [Fact]
    public void GetHashCode_ShouldBeSame_WhenSubfieldsAreEqual()
    {
        // Arrange
        Subfield subfield1 = Subfield.Create('a', "Test Value").Value;
        Subfield subfield2 = Subfield.Create('a', "Test Value").Value;

        // Act & Assert
        Assert.Equal(subfield1.GetHashCode(), subfield2.GetHashCode());
    }

    [Fact]
    public void Create_ShouldHandleSpecialCharacters_WhenValueContainsUnicode()
    {
        // Arrange
        var value = "Tëst Vålüé with ñ and € symbols";

        // Act
        KnResult<Subfield> result = Subfield.Create('a', value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value.Value);
    }

    [Fact]
    public void Create_ShouldHandleLongValues_WhenValueIsVeryLong()
    {
        // Arrange
        var longValue = new string('x', 1000);

        // Act
        KnResult<Subfield> result = Subfield.Create('a', longValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(longValue, result.Value.Value);
    }
}