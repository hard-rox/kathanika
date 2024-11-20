using HotChocolate;

namespace Kathanika.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    public string Id { get; private init; } = string.Empty;

    [GraphQLIgnore]
    public static bool operator ==(Entity? firstObject, Entity? secondObject)
    {
        return firstObject is not null
            && secondObject is not null
            && firstObject.Equals(secondObject);
    }

    [GraphQLIgnore]
    public static bool operator !=(Entity? firstObject, Entity? secondObject)
    {
        return !(firstObject == secondObject);
    }

    [GraphQLIgnore]
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != GetType()) return false;
        if (obj is not Entity entity) return false;

        return entity.Id == Id;
    }

    [GraphQLIgnore]
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    [GraphQLIgnore]
    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        if (other.GetType() != GetType()) return false;

        return other.Id == Id;
    }
}