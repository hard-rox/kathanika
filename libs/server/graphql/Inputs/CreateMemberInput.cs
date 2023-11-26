namespace Kathanika.GraphQL.Inputs;

public sealed class CreateMemberInput : InputObjectType<CreateMemberCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateMemberCommand> descriptor)
    {
        descriptor.Name(nameof(CreateMemberInput));
    }
}
