namespace Kathanika.Infrastructure.GraphQL.Inputs;

public sealed class CreateAuthorInput : InputObjectType<CreateAuthorCommand>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateAuthorCommand> descriptor)
    {
        descriptor.Name(nameof(CreateAuthorInput));
        descriptor.Field(x => x.DateOfBirth).Type<NonNullType<DateType>>();
        descriptor.Field(x => x.DateOfDeath).Type<DateType>();
    }
}