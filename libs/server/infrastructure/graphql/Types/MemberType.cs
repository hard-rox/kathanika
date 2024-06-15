namespace Kathanika.Infrastructure.Graphql.Types;

public sealed class MemberType : ObjectType<Member>
{
    protected override void Configure(IObjectTypeDescriptor<Member> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.FirstName);
        descriptor.Field(x => x.LastName);
        descriptor.Field(x => x.FullName);
        descriptor.Field(x => x.DateOfBirth);
        descriptor.Field(x => x.Address);
        descriptor.Field(x => x.ContactNumber);
        descriptor.Field(x => x.Email);
        descriptor.Field(x => x.Status);
        descriptor.Field(x => x.MembershipStartDateTime);
    }
}
