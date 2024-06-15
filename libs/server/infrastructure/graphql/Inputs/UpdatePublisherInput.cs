namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class UpdatePublisherInput : InputObjectType<UpdatePublisherCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<UpdatePublisherCommand> descriptor)
    {
        descriptor.Name(nameof(UpdatePublisherInput));
    }
}
