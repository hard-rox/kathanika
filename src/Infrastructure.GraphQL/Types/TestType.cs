namespace Kathanika.Infrastructure.GraphQL.Types
{
    public class TestType
    {
        public string Title { get; set; }

        public Author Author { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
    }
}
