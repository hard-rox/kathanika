namespace Kathanika.Infrastructure.Graphql.Bases;

public abstract class AbstractValueObjectType<T> : ObjectType<T> where T : ValueObject
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        descriptor.Ignore(x => x.GetAtomicValues());
    }
}
