namespace Kathanika.Infrastructure.GraphQL.Inputs;

public sealed class UpdateAuthorInput : InputObjectType<UpdateAuthorCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<UpdateAuthorCommand> descriptor)
    {
        descriptor.Name(nameof(UpdateAuthorInput));
        descriptor.Field(x => x.Data).Name("patch");
    }
}