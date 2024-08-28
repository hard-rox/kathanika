using HotChocolate.Types.Descriptors;

namespace Kathanika.Infrastructure.Graphql.GraphqlHelpers;

internal sealed class ApplicationNamingConvention : DefaultNamingConventions
{
    public override string GetTypeName(Type type, TypeKind kind)
    {
        string name = base.GetTypeName(type, kind);
        if (kind == TypeKind.InputObject
            && type.Name.EndsWith("Command")
            && name.EndsWith("CommandInput"))
            name = name.Replace("Command", string.Empty);

        return name;
    }
}
