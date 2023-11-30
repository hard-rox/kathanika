namespace Kathanika.GraphQL.Inputs;

public sealed class AddPublisherInput : InputObjectType<AddPublisherCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<AddPublisherCommand> descriptor)
    {
        descriptor.Name(nameof(AddPublisherInput));
    }
}
