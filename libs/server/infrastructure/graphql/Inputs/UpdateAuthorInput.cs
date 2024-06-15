namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class UpdateAuthorInput : InputObjectType<UpdateAuthorCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<UpdateAuthorCommand> descriptor)
    {
        descriptor.Name(nameof(UpdateAuthorInput));
    }
}
