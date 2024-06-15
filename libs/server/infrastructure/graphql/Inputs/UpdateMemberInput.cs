namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class UpdateMemberInput : InputObjectType<UpdateMemberCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<UpdateMemberCommand> descriptor)
    {
        descriptor.Name(nameof(UpdateMemberInput));
    }
}
