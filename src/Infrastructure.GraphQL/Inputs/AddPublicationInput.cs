namespace Kathanika.Infrastructure.GraphQL.Inputs;

public sealed class AddPublicationInput : InputObjectType<AddPublicationCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<AddPublicationCommand> descriptor)
    {
        descriptor.Name(nameof(AddPublicationInput));
    }
}
