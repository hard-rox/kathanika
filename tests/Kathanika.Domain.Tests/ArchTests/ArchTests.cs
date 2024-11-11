using NetArchTest.Rules;

namespace Kathanika.Domain.Tests.ArchTests;

public class ArchTests
{
    [Fact]
    public void PublicMethod_MustReturnResult()
    {
        List<Type> aggregatesAndEntities = Types.InAssembly(typeof(Entity).Assembly)
            .That()
            .Inherit(typeof(Entity))
            .Or()
            .Inherit(typeof(AggregateRoot))
            .And()
            .ArePublic()
            .GetTypes()
            .ToList();

        string[] aggregateMethods = typeof(AggregateRoot)
            .GetMethods()
            .Select(m => m.Name)
            .ToArray();

        var nonResultReturnTypes = aggregatesAndEntities
            .SelectMany(t => t.GetMethods())
            .Where(
                m => !(m.IsSpecialName && m.Name.StartsWith("get_"))
                    && !(m.ReturnType == typeof(Result)
                        || m.ReturnType.IsGenericType && m.ReturnType.GetGenericTypeDefinition().BaseType == typeof(Result))
                    && !aggregateMethods.Any(am => am == m.Name)
            )
            .Select(x => new { MethodName = x.Name, Type = x.DeclaringType?.Name })
            .ToList();

        Assert.Empty(nonResultReturnTypes);
    }
}