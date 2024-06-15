namespace Kathanika.Core.Domain.Primitives;

public abstract class DomainException(string message) : Exception(message)
{
}
