using System.Reflection;
using Kathanika.Domain.Primitives;
using NetArchTest.Rules;
using Xunit;

namespace Kathanika.Domain.Tests.ArchTests;

public class EntityValueObjectConstructorTests
{
    [Fact]
    public void AllEntities_MustHavePrivateParameterlessConstructor()
    {
        // Find all Entity and AggregateRoot implementations in the domain assembly
        IEnumerable<Type>? entityTypes = Types.InAssembly(typeof(Entity).Assembly)
            .That()
            .AreNotAbstract()
            .And()
            .Inherit(typeof(Entity))
            .Or()
            .Inherit(typeof(AggregateRoot))
            .GetTypes();

        List<string> typesWithoutPrivateConstructor = [];
        typesWithoutPrivateConstructor.AddRange(from type in entityTypes
            where !HasPrivateParameterlessConstructor(type)
            select type.FullName ?? type.Name);

        // Assert that all types have private parameterless constructors
        Assert.Empty(typesWithoutPrivateConstructor);
    }

    [Fact]
    public void AllValueObjects_MustHavePrivateParameterlessConstructor()
    {
        // Find all ValueObject implementations in the domain assembly
        IEnumerable<Type>? valueObjectTypes = Types.InAssembly(typeof(ValueObject).Assembly)
            .That()
            .Inherit(typeof(ValueObject))
            .GetTypes();

        List<string> typesWithoutPrivateConstructor = new List<string>();

        // Check each type for a private parameterless constructor
        foreach (Type type in valueObjectTypes)
        {
            if (!HasPrivateParameterlessConstructor(type))
            {
                typesWithoutPrivateConstructor.Add(type.FullName ?? type.Name);
            }
        }

        // Assert that all types have private parameterless constructors
        Assert.Empty(typesWithoutPrivateConstructor);
    }

    /// <summary>
    /// Checks if a type has a private parameterless constructor
    /// </summary>
    private static bool HasPrivateParameterlessConstructor(Type type)
    {
        // Look for a constructor with no parameters that is private
        ConstructorInfo? constructor = type.GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            Type.EmptyTypes,
            null);

        return constructor != null && constructor.IsPrivate;
    }
}