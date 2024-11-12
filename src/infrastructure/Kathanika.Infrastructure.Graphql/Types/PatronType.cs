using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Infrastructure.Graphql.Types;
public sealed class PatronType : ObjectType<Patron>
{
    protected override void Configure(IObjectTypeDescriptor<Patron> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Salutation);
        descriptor.Field(x => x.FirstName);
        descriptor.Field(x => x.Surname);
        descriptor.Field(x => x.Address);
        descriptor.Field(x => x.ContactNumber);
        descriptor.Field(x => x.Email);
        descriptor.Field(x => x.CardNumber);
        descriptor.Field(x => x.RegistrationDate);
        descriptor.Field(x => x.FullName);
    }
}