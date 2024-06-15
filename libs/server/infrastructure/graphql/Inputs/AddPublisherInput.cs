namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class AddPublisherInput : InputObjectType<AddPublisherCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<AddPublisherCommand> descriptor)
    {
        descriptor.Name(nameof(AddPublisherInput));
    }
}
