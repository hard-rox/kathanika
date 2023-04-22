namespace Kathanika.Infrastructure.GraphQL.Inputs;

public sealed class AddPublicationInput : ObjectType<AddPublicationCommand>
{
    protected override void Configure(IObjectTypeDescriptor<AddPublicationCommand> descriptor)
    {
        descriptor.Name(nameof(AddPublicationInput));
    }
}
