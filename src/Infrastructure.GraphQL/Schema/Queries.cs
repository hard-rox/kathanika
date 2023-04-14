using Kathanika.Infrastructure.GraphQL.Types;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public class Queries
{
    public TestType GetBook() =>
        new TestType()
        {
            Title = "C# in depth.",
            Author = new Author()
            {
                Name = "Jon Skeet"
            }
        };
}
