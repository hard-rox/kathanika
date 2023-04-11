namespace Kathanika.Domain.Aggregates.Book;

public class Book
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
}