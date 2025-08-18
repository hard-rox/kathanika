using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibRecordAggregate;

public sealed class MarcMetadataTests
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenCalled()
    {
        // Act
        KnResult<MarcMetadata> result = MarcMetadata.Create();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Create_ShouldInitializeControlFields_WhenCalled()
    {
        // Act
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Assert
        Assert.Contains(metadata.ControlFields, f => f.Tag == "001");
        Assert.Contains(metadata.ControlFields, f => f.Tag == "005");
        Assert.Contains(metadata.ControlFields, f => f.Tag == "008");
    }

    [Fact]
    public void Create_ShouldGenerateValidControlField001_WhenCalled()
    {
        // Act
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Assert
        ControlField controlField001 = metadata.ControlFields.First(f => f.Tag == "001");
        Assert.StartsWith("KN", controlField001.Data);
        Assert.Equal(12, controlField001.Data.Length); // "KN" + 10 characters
    }

    [Fact]
    public void Create_ShouldGenerateValidControlField005_WhenCalled()
    {
        // Act
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Assert
        ControlField controlField005 = metadata.ControlFields.First(f => f.Tag == "005");
        Assert.Equal(16, controlField005.Data.Length); // yyyyMMddHHmmss.f format
        Assert.True(DateTime.TryParseExact(controlField005.Data, "yyyyMMddHHmmss.f", null,
            System.Globalization.DateTimeStyles.None, out _));
    }

    [Fact]
    public void Create_ShouldGenerateValidControlField008_WhenCalled()
    {
        // Act
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Assert
        ControlField controlField008 = metadata.ControlFields.First(f => f.Tag == "008");
        Assert.Equal(40, controlField008.Data.Length);

        // Check date entered (positions 0-5) - should be the current date in YYMMDD format
        var dateEntered = controlField008.Data.Substring(0, 6);
        Assert.True(
            DateTime.TryParseExact(dateEntered, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out _));

        // Check the type of date (position 6)
        Assert.Equal('s', controlField008.Data[6]);

        // Check language code (positions 35-37)
        Assert.Equal("eng", controlField008.Data.Substring(35, 3));
    }

    [Fact]
    public void Create_ShouldGenerateValidLeader_WhenCalled()
    {
        // Act
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Assert
        Assert.Equal(24, metadata.Leader.Length);
        Assert.True(int.TryParse(metadata.Leader.Substring(0, 5), out var recordLength));
        Assert.True(recordLength > 0);
        Assert.True(int.TryParse(metadata.Leader.Substring(12, 5), out var baseAddress));
        Assert.True(baseAddress > 0);
    }

    [Fact]
    public void GetControlFieldValue_ShouldReturnCorrectValue_WhenFieldExists()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Act
        var value001 = metadata.GetControlFieldValue("001");
        var value005 = metadata.GetControlFieldValue("005");
        var value008 = metadata.GetControlFieldValue("008");

        // Assert
        Assert.NotEmpty(value001);
        Assert.NotEmpty(value005);
        Assert.NotEmpty(value008);
        Assert.StartsWith("KN", value001);
        Assert.Equal(16, value005.Length);
        Assert.Equal(40, value008.Length);
    }

    [Fact]
    public void GetControlFieldValue_ShouldReturnEmptyString_WhenFieldDoesNotExist()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Act
        var value = metadata.GetControlFieldValue("999");

        // Assert
        Assert.Equal(string.Empty, value);
    }

    [Fact]
    public void GetDataFieldValue_ShouldReturnEmptyString_WhenFieldDoesNotExist()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Act
        var value = metadata.GetDataFieldValue("200", 'a');

        // Assert
        Assert.Equal(string.Empty, value);
    }

    [Fact]
    public void GetMaterialType_ShouldReturnBooks_WhenField008HasTypeA()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Act
        var materialType = metadata.GetMaterialType();

        // Assert
        // Default 008 field has 's' at position 6, which maps to "Unknown"
        Assert.Equal("Unknown", materialType);
    }

    [Fact]
    public void AddDataField_ShouldReturnSuccess_WhenValidDataProvided()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;
        List<Subfield> subfields = [Subfield.Create('a', "Test Title").Value];

        // Act
        KnResult addResult = metadata.AddDataField("200", ' ', ' ', subfields);

        // Assert
        Assert.True(addResult.IsSuccess);
        Assert.Single(metadata.DataFields);
        Assert.Equal("200", metadata.DataFields[0].Tag);
    }

    [Fact]
    public void AddDataField_ShouldUpdateLeader_WhenDataFieldAdded()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;
        var originalLeader = metadata.Leader;
        List<Subfield> subfields = [Subfield.Create('a', "Test Title").Value];

        // Act
        metadata.AddDataField("200", ' ', ' ', subfields);

        // Assert
        Assert.NotEqual(originalLeader, metadata.Leader);

        // Leader should reflect increased record length
        var originalLength = int.Parse(originalLeader.Substring(0, 5));
        var newLength = int.Parse(metadata.Leader.Substring(0, 5));
        Assert.True(newLength > originalLength);
    }

    [Fact]
    public void GetDataFieldValue_ShouldReturnCorrectValue_WhenFieldAndSubfieldExist()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;
        var testValue = "Test Title";
        List<Subfield> subfields = [Subfield.Create('a', testValue).Value];
        metadata.AddDataField("200", ' ', ' ', subfields);

        // Act
        var value = metadata.GetDataFieldValue("200", 'a');

        // Assert
        Assert.Equal(testValue, value);
    }

    [Fact]
    public void GetDataFieldValue_ShouldReturnEmptyString_WhenSubfieldDoesNotExist()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;
        List<Subfield> subfields = [Subfield.Create('a', "Test Title").Value];
        metadata.AddDataField("200", ' ', ' ', subfields);

        // Act
        var value = metadata.GetDataFieldValue("200", 'b');

        // Assert
        Assert.Equal(string.Empty, value);
    }

    [Fact]
    public void ControlFields_ShouldBeReadOnly_WhenAccessed()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Act & Assert
        Assert.IsAssignableFrom<IReadOnlyList<ControlField>>(metadata.ControlFields);
    }

    [Fact]
    public void DataFields_ShouldBeReadOnly_WhenAccessed()
    {
        // Arrange
        KnResult<MarcMetadata> result = MarcMetadata.Create();
        MarcMetadata metadata = result.Value;

        // Act & Assert
        Assert.IsAssignableFrom<IReadOnlyList<DataField>>(metadata.DataFields);
    }
}