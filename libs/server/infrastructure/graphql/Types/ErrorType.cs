namespace Kathanika.Infrastructure.Graphql.Types;

public class KnErrorType : ObjectType<KnError>;
public class ValidationErrorType : ObjectType<ValidationError>;

public class ErrorType : UnionType
{
    protected override void Configure(IUnionTypeDescriptor descriptor)
    {
        descriptor.Type<KnErrorType>();
        descriptor.Type<ValidationErrorType>();
    }
}
