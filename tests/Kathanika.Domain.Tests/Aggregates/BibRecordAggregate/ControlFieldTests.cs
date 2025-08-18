using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibRecordAggregate;

public sealed class ControlFieldTests
{
    [Theory]
    [InlineData("001", "12345")]
    [InlineData("003", "DLC")]
    [InlineData("005", "20231201120000.0")]
    [InlineData("008", "231201s2023    nyu           000 0 eng d")]
    [InlineData("009", "test data")]
    public void Create_ShouldReturnSuccess_WhenValidTagAndDataProvided(string tag, string data)
    {
        // Act
        KnResult<ControlField> result = ControlField.Create(tag, data);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(tag, result.Value.Tag);
        Assert.Equal(data, result.Value.Data);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Create_ShouldReturnFailure_WhenTagIsNullOrWhitespace(string? tag)
    {
        // Act
        KnResult<ControlField> result = ControlField.Create(tag, "test data");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "ControlField.EmptyTag");
    }

    [Theory]
    [InlineData("000")]
    [InlineData("010")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("1234")]
    [InlineData("abc")]
    [InlineData("00a")]
    [InlineData("0a1")]
    public void Create_ShouldReturnFailure_WhenTagIsInvalid(string invalidTag)
    {
        // Act
        KnResult<ControlField> result = ControlField.Create(invalidTag, "test data");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "ControlField.InvalidTag");
        Assert.Contains(result.Errors, e => e.Message.Contains(invalidTag));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Create_ShouldReturnFailure_WhenDataIsNullOrWhitespace(string? data)
    {
        // Act
        KnResult<ControlField> result = ControlField.Create("001", data);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "ControlField.EmptyData");
    }

    [Fact]
    public void GetAtomicValues_ShouldReturnTagAndData_WhenCalled()
    {
        // Arrange
        var tag = "001";
        var data = "12345";
        KnResult<ControlField> result = ControlField.Create(tag, data);
        ControlField controlField = result.Value;

        // Act
        List<object?> atomicValues = controlField.GetAtomicValues().ToList();

        // Assert
        Assert.Equal(2, atomicValues.Count);
        Assert.Equal(tag, atomicValues[0]);
        Assert.Equal(data, atomicValues[1]);
    }

    [Fact]
    public void Equality_ShouldReturnTrue_WhenControlFieldsHaveSameTagAndData()
    {
        // Arrange
        ControlField controlField1 = ControlField.Create("001", "12345").Value;
        ControlField controlField2 = ControlField.Create("001", "12345").Value;

        // Act & Assert
        Assert.Equal(controlField1, controlField2);
        Assert.True(controlField1 == controlField2);
        Assert.False(controlField1 != controlField2);
    }

    [Fact]
    public void Equality_ShouldReturnFalse_WhenControlFieldsHaveDifferentTags()
    {
        // Arrange
        ControlField controlField1 = ControlField.Create("001", "12345").Value;
        ControlField controlField2 = ControlField.Create("003", "12345").Value;

        // Act & Assert
        Assert.NotEqual(controlField1, controlField2);
        Assert.False(controlField1 == controlField2);
        Assert.True(controlField1 != controlField2);
    }

    [Fact]
    public void Equality_ShouldReturnFalse_WhenControlFieldsHaveDifferentData()
    {
        // Arrange
        ControlField controlField1 = ControlField.Create("001", "12345").Value;
        ControlField controlField2 = ControlField.Create("001", "67890").Value;

        // Act & Assert
        Assert.NotEqual(controlField1, controlField2);
        Assert.False(controlField1 == controlField2);
        Assert.True(controlField1 != controlField2);
    }

    [Fact]
    public void GetHashCode_ShouldBeSame_WhenControlFieldsAreEqual()
    {
        // Arrange
        ControlField controlField1 = ControlField.Create("001", "12345").Value;
        ControlField controlField2 = ControlField.Create("001", "12345").Value;

        // Act & Assert
        Assert.Equal(controlField1.GetHashCode(), controlField2.GetHashCode());
    }
}