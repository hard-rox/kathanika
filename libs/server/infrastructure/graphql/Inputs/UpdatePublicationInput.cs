namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class UpdatePublicationInput : InputObjectType<UpdatePublicationCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<UpdatePublicationCommand> descriptor)
    {
        descriptor.Name(nameof(UpdatePublicationInput));
    }
}
