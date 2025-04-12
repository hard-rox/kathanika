using HotChocolate.Types.Descriptors;

namespace Kathanika.Infrastructure.Graphql.GraphqlHelpers;

// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class ApplicationNamingConvention : DefaultNamingConventions
{
    public override string GetTypeName(Type type, TypeKind kind)
    {
        var name = base.GetTypeName(type, kind);
        if (kind == TypeKind.InputObject
            && type.Name.EndsWith("Command")
            && name.EndsWith("CommandInput"))
            name = name.Replace("Command", string.Empty);

        if (kind == TypeKind.InputObject
            && type.Name.EndsWith("Dto")
            && name.EndsWith("DtoInput"))
            name = name.Replace("Dto", string.Empty);

        return name;
    }
}