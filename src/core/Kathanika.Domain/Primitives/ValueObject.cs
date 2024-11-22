using HotChocolate;

namespace Kathanika.Domain.Primitives;

public abstract record ValueObject
{
    [GraphQLIgnore]
    public virtual bool Equals(ValueObject? other)
    {
        return other is not null
               && ValuesAreEqual(other);
    }

    [GraphQLIgnore]
    public abstract IEnumerable<object?> GetAtomicValues();

    [GraphQLIgnore]
    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    [GraphQLIgnore]
    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(default(int), HashCode.Combine);
    }
}