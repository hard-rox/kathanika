namespace Kathanika.GraphQL.Inputs;

public sealed class AcquirePublicationInput : InputObjectType<AcquirePublicationCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<AcquirePublicationCommand> descriptor)
    {
        descriptor.Name(nameof(AcquirePublicationInput));
    }
}
