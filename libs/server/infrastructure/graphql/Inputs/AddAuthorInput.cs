namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class AddAuthorInput : InputObjectType<AddAuthorCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<AddAuthorCommand> descriptor)
    {
        descriptor.Name(nameof(AddAuthorInput));
    }
}
