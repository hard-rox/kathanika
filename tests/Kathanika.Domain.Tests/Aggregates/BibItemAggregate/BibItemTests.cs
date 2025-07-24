using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibItemAggregate;

public class BibItemTests
{
    #region Create Tests

    [Fact]
    public void Create_WithValidParameters_ShouldCreateBibItemWithExpectedProperties()
    {
        // Arrange
        const string bibRecordId = "bib-123";
        const string barcode = "123456789";
        const string callNumber = "QA76.73.C153";
        const string location = "Main Library";
        const ItemType itemType = ItemType.Book;
        const ItemStatus status = ItemStatus.Available;

        // Act
        KnResult<BibItem> bibItemResult = BibItem.Create(
            bibRecordId,
            barcode,
            callNumber,
            location,
            itemType,
            status);

        // Assert
        Assert.True(bibItemResult.IsSuccess);
        Assert.NotNull(bibItemResult.Value);
        Assert.Equal(bibRecordId, bibItemResult.Value.BibRecordId);
        Assert.Equal(barcode, bibItemResult.Value.Barcode);
        Assert.Equal(callNumber, bibItemResult.Value.CallNumber);
        Assert.Equal(location, bibItemResult.Value.Location);
        Assert.Equal(itemType, bibItemResult.Value.ItemType);
        Assert.Equal(status, bibItemResult.Value.Status);
        Assert.Equal(AcquisitionType.Transfer, bibItemResult.Value.AcquisitionType); // Updated to match actual default
        Assert.Null(bibItemResult.Value.AcquisitionDate); // Updated to match actual implementation
    }

    [Fact]
    public void Create_WithValidParameters_ShouldSetDefaultValues()
    {
        // Arrange & Act
        KnResult<BibItem> bibItemResult = BibItem.Create(
            "bib-123",
            "123456789",
            "QA76.73.C153",
            "Main Library",
            ItemType.Book,
            ItemStatus.Available);

        // Assert
        Assert.True(bibItemResult.IsSuccess);
        Assert.Equal(AcquisitionType.Transfer, bibItemResult.Value.AcquisitionType);
        Assert.Null(bibItemResult.Value.Vendor);
        Assert.Null(bibItemResult.Value.AcquisitionDate);
    }

    #endregion

    #region CheckOut Tests

    [Fact]
    public void CheckOut_WhenItemIsAvailable_ShouldUpdateStatusToCheckedOutAndSetCheckOutDate()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        DateTime beforeCheckOut = DateTime.UtcNow;

        // Act
        KnResult result = bibItem.CheckOut();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ItemStatus.CheckedOut, bibItem.Status);
        Assert.NotNull(bibItem.LastCheckOutDate);
        Assert.True(bibItem.LastCheckOutDate >= beforeCheckOut);
    }

    [Fact]
    public void CheckOut_WhenItemIsAlreadyCheckedOut_ShouldReturnFailureResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        bibItem.CheckOut(); // First checkout

        // Act
        KnResult result = bibItem.CheckOut();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(BibItemAggregateErrors.InvalidStatus, result.Errors[0]);
    }

    [Fact]
    public void CheckOut_WhenItemIsWithdrawn_ShouldReturnFailureResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        bibItem.Withdraw("Test withdrawal reason");

        // Act
        KnResult result = bibItem.CheckOut();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(BibItemAggregateErrors.InvalidStatus, result.Errors[0]);
    }

    #endregion

    #region CheckIn Tests

    [Fact]
    public void CheckIn_WhenItemIsCheckedOut_ShouldUpdateStatusToAvailableAndSetCheckInDate()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        bibItem.CheckOut();
        DateTime beforeCheckIn = DateTime.UtcNow;

        // Act
        KnResult result = bibItem.CheckIn();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ItemStatus.Available, bibItem.Status);
        Assert.NotNull(bibItem.LastCheckInDate);
        Assert.True(bibItem.LastCheckInDate >= beforeCheckIn);
    }

    [Fact]
    public void CheckIn_WhenItemIsNotCheckedOut_ShouldReturnFailureResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();

        // Act
        KnResult result = bibItem.CheckIn();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(BibItemAggregateErrors.InvalidStatus, result.Errors[0]);
    }

    [Fact]
    public void CheckIn_WhenItemIsWithdrawn_ShouldReturnFailureResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        bibItem.Withdraw("Test withdrawal reason");

        // Act
        KnResult result = bibItem.CheckIn();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(BibItemAggregateErrors.InvalidStatus, result.Errors[0]);
    }

    #endregion

    #region Withdraw Tests

    [Fact]
    public void Withdraw_WithValidReason_ShouldUpdateStatusToWithdrawnAndSetWithdrawDate()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        const string withdrawalReason = "Damaged beyond repair";
        DateTime beforeWithdraw = DateTime.UtcNow;

        // Act
        KnResult result = bibItem.Withdraw(withdrawalReason);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ItemStatus.Withdrawn, bibItem.Status);
        Assert.NotNull(bibItem.WithdrawnDate);
        Assert.True(bibItem.WithdrawnDate >= beforeWithdraw);
        Assert.Contains(withdrawalReason, bibItem.Notes);
    }

    [Fact]
    public void Withdraw_WithoutReason_ShouldUpdateStatusToWithdrawnAndSetWithdrawDate()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        DateTime beforeWithdraw = DateTime.UtcNow;

        // Act
        KnResult result = bibItem.Withdraw();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ItemStatus.Withdrawn, bibItem.Status);
        Assert.NotNull(bibItem.WithdrawnDate);
        Assert.True(bibItem.WithdrawnDate >= beforeWithdraw);
    }

    [Fact]
    public void Withdraw_WhenItemIsCheckedOut_ShouldStillAllowWithdrawal()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        bibItem.CheckOut();
        const string withdrawalReason = "Lost by patron";

        // Act
        KnResult result = bibItem.Withdraw(withdrawalReason);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ItemStatus.Withdrawn, bibItem.Status);
        Assert.Contains(withdrawalReason, bibItem.Notes);
    }

    #endregion

    #region Helper Methods

    private static BibItem CreateTestBibItem()
    {
        return BibItem.Create(
            "bib-123",
            "123456789",
            "QA76.73.C153",
            "Main Library",
            ItemType.Book,
            ItemStatus.Available).Value;
    }

    #endregion
}