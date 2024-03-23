namespace Kathanika.GraphQL.Inputs;

public sealed class PurchasePublicationInput : InputObjectType<PurchasePublicationCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<PurchasePublicationCommand> descriptor)
    {
        descriptor.Name(nameof(PurchasePublicationInput));
    }
}
