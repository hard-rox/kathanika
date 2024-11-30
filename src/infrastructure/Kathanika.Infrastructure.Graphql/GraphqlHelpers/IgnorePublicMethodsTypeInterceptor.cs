using System.Reflection;
using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Definitions;

namespace Kathanika.Infrastructure.Graphql.GraphqlHelpers;

// ReSharper disable once ClassNeverInstantiated.Global
internal class IgnorePublicMethodsTypeInterceptor : TypeInterceptor
{
    public override void OnAfterInitialize(ITypeDiscoveryContext discoveryContext, DefinitionBase definition)
    {
        if (definition is not ObjectTypeDefinition objectTypeDefinition) return;
        var isEntityOrAggregate = objectTypeDefinition.RuntimeType.BaseType == typeof(Entity) ||
                                  objectTypeDefinition.RuntimeType.BaseType == typeof(AggregateRoot);
        if (!isEntityOrAggregate)
            return;

        IEnumerable<string> methodNames = objectTypeDefinition.RuntimeType
            .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(method => !method.IsSpecialName)
            .Select(method => method.Name);

        List<ObjectFieldDefinition> ignorableFields = objectTypeDefinition.Fields
            .Where(x => methodNames.Distinct()
                .Contains(x.Name, StringComparer.OrdinalIgnoreCase))
            .ToList();
        ignorableFields.ForEach(x =>
        {
            x.Ignore = true;
        });
    }
}