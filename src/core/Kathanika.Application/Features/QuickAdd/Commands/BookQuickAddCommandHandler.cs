using Kathanika.Domain.Aggregates.BibItemAggregate;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.QuickAdd.Commands;

internal sealed class BookQuickAddCommandHandler(
    IBibRecordRepository bibRecordRepository,
    IBibItemRepository bibItemRepository)
    : IRequestHandler<BookQuickAddCommand, KnResult<BibRecord>>
{
    public async Task<KnResult<BibRecord>> Handle(BookQuickAddCommand request, CancellationToken cancellationToken)
    {
        // Create the BibRecord with the provided metadata
        KnResult<BibRecord> bibRecordResult = BibRecord.CreateBookRecord(
            request.Title,
            request.Author,
            request.Isbn,
            request.Publisher,
            request.YearOfPublication,
            request.Language,
            request.NumberOfPages,
            request.CoverImageId
        );

        if (bibRecordResult.IsFailure)
            return bibRecordResult;

        // Add the BibRecord to the repository
        BibRecord createdBibRecord = await bibRecordRepository.AddAsync(bibRecordResult.Value, cancellationToken);

        // Create the specified number of BibItems
        List<BibItem> bibItems = [];
        List<KnError> errors = [];

        var callNumber = $"{ExtractTitlePrefix(request.Title)} {ExtractLastName(request.Author)}";
        for (var i = 1; i <= request.NumberOfCopies; i++)
        {
            // Generate a unique barcode for each copy
            var barcode = await GenerateUniqueBarcodeAsync(cancellationToken);

            // Default location for quick-added items
            const string location = "General Collection";

            KnResult<BibItem> bibItemResult = BibItem.Create(
                bibRecordId: createdBibRecord.Id,
                barcode: barcode,
                callNumber: callNumber,
                location: location,
                itemType: ItemType.Book,
                status: ItemStatus.Available,
                notes: $"Quick add copy {i} of {request.NumberOfCopies}",
                acquisitionType: AcquisitionType.Purchase,
                acquisitionDate: DateOnly.FromDateTime(DateTime.UtcNow)
            );

            if (bibItemResult.IsFailure)
            {
                errors.AddRange(bibItemResult.Errors);
                continue;
            }

            BibItem createdBibItem = await bibItemRepository.AddAsync(bibItemResult.Value, cancellationToken);
            bibItems.Add(createdBibItem);
        }

        return bibItems.Count == 0 ? KnResult.Failure<BibRecord>(errors) : bibRecordResult;
    }

    private async Task<string> GenerateUniqueBarcodeAsync(CancellationToken cancellationToken)
    {
        string barcode;
        bool exists;

        do
        {
            // Generate a 10-digit barcode starting with "KB" (Kathanika Book)
            var randomNumber = Random.Shared.Next(10000000, 99999999).ToString();
            barcode = $"KB{randomNumber}";

            exists = await bibItemRepository.ExistsAsync(
                x => x.Barcode == barcode,
                cancellationToken);
        } while (exists);

        return barcode;
    }

    private static string ExtractLastName(string fullName)
    {
        // Handle "Last, First" or "First Last" formats
        var nameParts = fullName.Split(',', StringSplitOptions.RemoveEmptyEntries);

        if (nameParts.Length > 1)
        {
            // "Last, First" format
            return nameParts[0].Trim().Replace(" ", "").ToUpperInvariant();
        }

        // "First Last" format - take the last word
        var words = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return words.Length > 0
            ? words[^1].Replace(" ", "").ToUpperInvariant()
            : fullName.Replace(" ", "").ToUpperInvariant();
    }

    private static string ExtractTitlePrefix(string title)
    {
        // Remove common articles and take the first 3 characters
        var cleanTitle = title;

        string[] articles = ["the ", "a ", "an "];
        foreach (var article in articles)
        {
            if (!cleanTitle.StartsWith(article, StringComparison.OrdinalIgnoreCase)) continue;
            cleanTitle = cleanTitle[article.Length..];
            break;
        }

        // Take the first 3 characters, pad if necessary
        cleanTitle = cleanTitle.Replace(" ", "").ToUpperInvariant();
        return cleanTitle.Length >= 3
            ? cleanTitle[..3]
            : cleanTitle.PadRight(3, 'X');
    }
}