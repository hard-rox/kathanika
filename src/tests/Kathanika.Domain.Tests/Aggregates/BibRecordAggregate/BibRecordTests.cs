using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Domain.Primitives;
using Xunit;

namespace Kathanika.Domain.Tests.Aggregates.BibRecordAggregate;

/// <summary>
/// Unit tests for BibRecord aggregate root.
/// Tests MARC21 integration and business logic validation.
/// </summary>
public sealed class BibRecordTests
{
    [Fact]
    public void CreateWithMarcMetadata_WithValidData_ShouldSucceed()
    {
        // Arrange
        var title = "Test Book Title";
        var author = "Test Author";
        var marcMetadata = CreateValidMarcMetadata();

        // Act
        var result = BibRecord.CreateWithMarcMetadata(title, author, marcMetadata);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(title, result.Value.Title);
        Assert.Equal(author, result.Value.Author);
        Assert.NotNull(result.Value.MarcMetadata);
        Assert.Equal(marcMetadata.Leader, result.Value.MarcMetadata.Leader);
    }

    [Fact]
    public void CreateWithMarcMetadata_ExtractsISBNsFromMarc_ShouldSucceed()
    {
        // Arrange
        var title = "Test Book Title";
        var author = "Test Author";
        var marcMetadata = CreateMarcMetadataWithISBN();

        // Act
        var result = BibRecord.CreateWithMarcMetadata(title, author, marcMetadata);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value.InternationalStandardBookNumbers);
        Assert.Equal("9781234567890", result.Value.InternationalStandardBookNumbers.First());
    }

    [Fact]
    public void CreateWithMarcMetadata_WithMissingTitleField_ShouldFail()
    {
        // Arrange
        var title = "Test Book Title";
        var author = "Test Author";
        var marcMetadata = CreateMarcMetadataWithoutTitle();

        // Act
        var result = BibRecord.CreateWithMarcMetadata(title, author, marcMetadata);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("245", result.Error.Message);
        Assert.Contains("Title Statement", result.Error.Message);
    }

    [Fact]
    public void UpdateMarcMetadata_WithValidData_ShouldSucceed()
    {
        // Arrange
        var bibRecord = CreateValidBibRecord();
        var newMarcMetadata = CreateValidMarcMetadata();

        // Act
        var result = bibRecord.UpdateMarcMetadata(newMarcMetadata);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newMarcMetadata.Leader, bibRecord.MarcMetadata.Leader);
    }

    [Fact]
    public void UpdateMarcMetadata_UpdatesISBNsFromMarc_ShouldSucceed()
    {
        // Arrange
        var bibRecord = CreateValidBibRecord();
        var newMarcMetadata = CreateMarcMetadataWithMultipleISBNs();

        // Act
        var result = bibRecord.UpdateMarcMetadata(newMarcMetadata);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, bibRecord.InternationalStandardBookNumbers.Count);
        Assert.Contains("9781234567890", bibRecord.InternationalStandardBookNumbers);
        Assert.Contains("9780987654321", bibRecord.InternationalStandardBookNumbers);
    }

    [Fact]
    public void CreateBookRecord_WithBasicData_ShouldSucceed()
    {
        // Arrange
        var title = "Simple Book";
        var author = "Simple Author";
        var isbn = "9781234567890";
        var publisher = "Test Publisher";
        var publicationYear = 2025;
        var language = "English";
        var numberOfPages = 300L;
        var edition = "First Edition";
        var description = "A test book";
        var coverImageId = "image123";

        // Act
        var result = BibRecord.CreateBookRecord(
            title, author, isbn, publisher, publicationYear,
            language, numberOfPages, edition, description, coverImageId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(title, result.Value.Title);
        Assert.Equal(author, result.Value.Author);
        Assert.Equal(description, result.Value.Note);
        Assert.Equal(coverImageId, result.Value.CoverImageId);
        Assert.Null(result.Value.MarcMetadata);
    }
    
    [Fact]
    public void Create_WithValidLeaderAndFields_ShouldSucceed()
    {
        // Arrange
        var leader = "00000nam a2200000 a 4500";
        var controlFields = new List<ControlField>
        {
            ControlField.Create("001", "12345").Value,
            ControlField.Create("005", "20250808120000.0").Value
        };
        var dataFields = new List<DataField>
        {
            DataField.Create("245", '1', '0', new List<Subfield>
            {
                Subfield.Create('a', "Test Title").Value
            }).Value
        };

        // Act
        var result = MarcMetadata.Create(leader, controlFields, dataFields);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(leader, result.Value.Leader);
        Assert.Equal(2, result.Value.ControlFields.Count);
        Assert.Equal(1, result.Value.DataFields.Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData("12345")]
    [InlineData("00000nam a2200000 a 45001")] // 25 characters
    [InlineData("00000nam a2200000 a 450")] // 23 characters
    public void Create_WithInvalidLeader_ShouldFail(string invalidLeader)
    {
        // Arrange
        var controlFields = new List<ControlField>
        {
            ControlField.Create("001", "12345").Value,
            ControlField.Create("005", "20250808120000.0").Value
        };
        var dataFields = new List<DataField>
        {
            DataField.Create("245", '1', '0', new List<Subfield>
            {
                Subfield.Create('a', "Test Title").Value
            }).Value
        };

        // Act
        var result = MarcMetadata.Create(invalidLeader, controlFields, dataFields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(BibRecordAggregateErrors.LeaderInvalid, result.Error);
    }

    [Fact]
    public void Create_WithMissingRequiredControlFields_ShouldFail()
    {
        // Arrange
        var leader = "00000nam a2200000 a 4500";
        var controlFields = new List<ControlField>
        {
            ControlField.Create("003", "DLC").Value // Missing 001 and 005
        };
        var dataFields = new List<DataField>
        {
            DataField.Create("245", '1', '0', new List<Subfield>
            {
                Subfield.Create('a', "Test Title").Value
            }).Value
        };

        // Act
        var result = MarcMetadata.Create(leader, controlFields, dataFields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(BibRecordAggregateErrors.ControlNumberInvalid, result.Error);
    }

    [Fact]
    public void Create_WithDuplicateNonRepeatableControlFields_ShouldFail()
    {
        // Arrange
        var leader = "00000nam a2200000 a 4500";
        var controlFields = new List<ControlField>
        {
            ControlField.Create("001", "12345").Value,
            ControlField.Create("001", "67890").Value, // Duplicate 001
            ControlField.Create("005", "20250808120000.0").Value
        };
        var dataFields = new List<DataField>
        {
            DataField.Create("245", '1', '0', new List<Subfield>
            {
                Subfield.Create('a', "Test Title").Value
            }).Value
        };

        // Act
        var result = MarcMetadata.Create(leader, controlFields, dataFields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(BibRecordAggregateErrors.ControlNumberInvalid, result.Error);
    }
    
    [Theory]
    [InlineData("001", "12345")]
    [InlineData("003", "DLC")]
    [InlineData("005", "20250808120000.0")]
    [InlineData("008", "1234567890123456789012345678901234567890")] // 40 chars
    public void Create_WithValidTagAndData_ShouldSucceed(string tag, string data)
    {
        // Act
        var result = ControlField.Create(tag, data);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(tag, result.Value.Tag);
        Assert.Equal(data, result.Value.Data);
    }

    [Theory]
    [InlineData("000", "data")] // Invalid tag
    [InlineData("010", "data")] // Data field tag
    [InlineData("abc", "data")] // Non-numeric tag
    [InlineData("", "data")] // Empty tag
    public void Create_WithInvalidTag_ShouldFail(string invalidTag, string data)
    {
        // Act
        var result = ControlField.Create(invalidTag, data);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("Control field tag must be 001-009", result.Error.Message);
    }

    [Fact]
    public void Create_WithEmptyData_ShouldFail()
    {
        // Act
        var result = ControlField.Create("001", "");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("data cannot be empty", result.Error.Message);
    }

    [Fact]
    public void Create_WithInvalidDateTimeFormat_ShouldFail()
    {
        // Act
        var result = ControlField.Create("005", "invalid-datetime");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(BibRecordAggregateErrors.DateTimeOfLatestTransactionInvalid, result.Error);
    }

    [Fact]
    public void Create_WithInvalidFixedLengthDataElements_ShouldFail()
    {
        // Act
        var result = ControlField.Create("008", "short"); // Not 40 characters

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(BibRecordAggregateErrors.FixedLengthDataElementsInvalid, result.Error);
    }

    [Theory]
    [InlineData("001", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt")] // > 50 chars
    public void Create_WithControlNumberTooLong_ShouldFail(string tag, string longData)
    {
        // Act
        var result = ControlField.Create(tag, longData);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(BibRecordAggregateErrors.ControlNumberInvalid, result.Error);
    }
    
    [Theory]
    [InlineData("020", '1', '0')]
    [InlineData("245", '1', '4')]
    [InlineData("999", ' ', ' ')]
    public void Create_WithValidTagAndIndicators_ShouldSucceed(string tag, char ind1, char ind2)
    {
        // Arrange
        var subfields = new List<Subfield>
        {
            Subfield.Create('a', "Test value").Value
        };

        // Act
        var result = DataField.Create(tag, ind1, ind2, subfields);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(tag, result.Value.Tag);
        Assert.Equal(ind1, result.Value.Indicator1);
        Assert.Equal(ind2, result.Value.Indicator2);
        Assert.Single(result.Value.Subfields);
    }

    [Theory]
    [InlineData("009", '1', '0')] // Control field tag
    [InlineData("000", '1', '0')] // Invalid tag
    [InlineData("abc", '1', '0')] // Non-numeric tag
    public void Create_WithInvalidTag_ShouldFail(string invalidTag, char ind1, char ind2)
    {
        // Arrange
        var subfields = new List<Subfield>
        {
            Subfield.Create('a', "Test value").Value
        };

        // Act
        var result = DataField.Create(invalidTag, ind1, ind2, subfields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("Data field tag must be 010-999", result.Error.Message);
    }

    [Theory]
    [InlineData('a', '0')] // Letter indicator
    [InlineData('!', '0')] // Special character indicator
    public void Create_WithInvalidIndicators_ShouldFail(char invalidInd1, char validInd2)
    {
        // Arrange
        var subfields = new List<Subfield>
        {
            Subfield.Create('a', "Test value").Value
        };

        // Act
        var result = DataField.Create("245", invalidInd1, validInd2, subfields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("Indicator1 must be digit 0-9 or space", result.Error.Message);
    }

    [Fact]
    public void Create_WithNoSubfields_ShouldFail()
    {
        // Arrange
        var emptySubfields = new List<Subfield>();

        // Act
        var result = DataField.Create("245", '1', '0', emptySubfields);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("must contain at least one subfield", result.Error.Message);
    }
    
    [Theory]
    [InlineData('a', "Author name")]
    [InlineData('z', "Notes")]
    [InlineData('0', "Authority record control number")]
    [InlineData('9', "Local subfield")]
    [InlineData('!', "Special subfield")]
    [InlineData('$', "Dollar subfield")]
    [InlineData('%', "Percent subfield")]
    public void Create_WithValidCodeAndValue_ShouldSucceed(char code, string value)
    {
        // Act
        var result = Subfield.Create(code, value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(code, result.Value.Code);
        Assert.Equal(value, result.Value.Value);
    }

    [Theory]
    [InlineData('A')] // Uppercase letter
    [InlineData('@')] // Invalid special character
    [InlineData('#')] // Invalid special character
    [InlineData(' ')] // Space
    public void Create_WithInvalidCode_ShouldFail(char invalidCode)
    {
        // Act
        var result = Subfield.Create(invalidCode, "Test value");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("Subfield code must be a-z, 0-9, !, $, or %", result.Error.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Create_WithEmptyValue_ShouldFail(string emptyValue)
    {
        // Act
        var result = Subfield.Create('a', emptyValue);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("value cannot be empty", result.Error.Message);
    }

    private static MarcMetadata CreateValidMarcMetadata()
    {
        var controlFields = new List<ControlField>
        {
            ControlField.Create("001", "12345").Value,
            ControlField.Create("005", "20250808120000.0").Value,
            ControlField.Create("008", "1234567890123456789012345678901234567890").Value
        };

        var dataFields = new List<DataField>
        {
            DataField.Create("245", '1', '0', new List<Subfield>
            {
                Subfield.Create('a', "Test Book Title").Value,
                Subfield.Create('c', "Test Author").Value
            }).Value,
            DataField.Create("100", '1', ' ', new List<Subfield>
            {
                Subfield.Create('a', "Test Author").Value
            }).Value
        };

        return MarcMetadata.Create("00000nam a2200000 a 4500", controlFields, dataFields).Value;
    }

    private static MarcMetadata CreateMarcMetadataWithISBN()
    {
        var controlFields = new List<ControlField>
        {
            ControlField.Create("001", "12345").Value,
            ControlField.Create("005", "20250808120000.0").Value
        };

        var dataFields = new List<DataField>
        {
            DataField.Create("245", '1', '0', new List<Subfield>
            {
                Subfield.Create('a', "Test Book Title").Value
            }).Value,
            DataField.Create("020", ' ', ' ', new List<Subfield>
            {
                Subfield.Create('a', "9781234567890").Value
            }).Value
        };

        return MarcMetadata.Create("00000nam a2200000 a 4500", controlFields, dataFields).Value;
    }

    private static MarcMetadata CreateMarcMetadataWithMultipleISBNs()
    {
        var controlFields = new List<ControlField>
        {
            ControlField.Create("001", "12345").Value,
            ControlField.Create("005", "20250808120000.0").Value
        };

        var dataFields = new List<DataField>
        {
            DataField.Create("245", '1', '0', new List<Subfield>
            {
                Subfield.Create('a', "Test Book Title").Value
            }).Value,
            DataField.Create("020", ' ', ' ', new List<Subfield>
            {
                Subfield.Create('a', "9781234567890").Value
            }).Value,
            DataField.Create("020", ' ', ' ', new List<Subfield>
            {
                Subfield.Create('a', "9780987654321").Value
            }).Value
        };

        return MarcMetadata.Create("00000nam a2200000 a 4500", controlFields, dataFields).Value;
    }

    private static MarcMetadata CreateMarcMetadataWithoutTitle()
    {
        var controlFields = new List<ControlField>
        {
            ControlField.Create("001", "12345").Value,
            ControlField.Create("005", "20250808120000.0").Value
        };

        var dataFields = new List<DataField>
        {
            DataField.Create("100", '1', ' ', new List<Subfield>
            {
                Subfield.Create('a', "Test Author").Value
            }).Value
            // Missing 245 field
        };

        return MarcMetadata.Create("00000nam a2200000 a 4500", controlFields, dataFields).Value;
    }

    private static BibRecord CreateValidBibRecord()
    {
        var marcMetadata = CreateValidMarcMetadata();
        return BibRecord.CreateWithMarcMetadata("Test Title", "Test Author", marcMetadata).Value;
    }
}
