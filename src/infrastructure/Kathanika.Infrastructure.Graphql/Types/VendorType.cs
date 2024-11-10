using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Infrastructure.Graphql.Types;

public sealed class VendorType : ObjectType<Vendor>
{
    protected override void Configure(IObjectTypeDescriptor<Vendor> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Name);
        descriptor.Field(x => x.Address);
        descriptor.Field(x => x.ContactNumber);
        descriptor.Field(x => x.Email);
        descriptor.Field(x => x.Website);
        descriptor.Field(x => x.AccountDetail);
        descriptor.Field(x => x.ContactPersonName);
        descriptor.Field(x => x.ContactPersonEmail);
        descriptor.Field(x => x.ContactPersonPhone);
        descriptor.Field(x => x.Status);
    }
}
