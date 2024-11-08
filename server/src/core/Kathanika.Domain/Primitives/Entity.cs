namespace Kathanika.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    public string Id { get; private init; } = string.Empty;

    public static bool operator ==(Entity? firstObject, Entity? secondObject)
    {
        return firstObject is not null
            && secondObject is not null
            && firstObject.Equals(secondObject);
    }

    public static bool operator !=(Entity? firstObject, Entity? secondObject)
    {
        return !(firstObject == secondObject);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != GetType()) return false;
        if (obj is not Entity entity) return false;

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        if (other.GetType() != GetType()) return false;

        return other.Id == Id;
    }
}
