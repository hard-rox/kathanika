using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Infrastructure.Graphql.Types;

public sealed class BibRecordType : ObjectType<BibRecord>
{
    protected override void Configure(IObjectTypeDescriptor<BibRecord> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Leader);
        descriptor.Field(x => x.ControlNumber);
        descriptor.Field(x => x.ControlNumberIdentifier);
        descriptor.Field(x => x.DateAndTimeOfLatestTransaction);
        descriptor.Field(x => x.FixedLengthDataElements);
        descriptor.Field(x => x.CatalogingSource);
    }
}

public sealed class CatalogingSourceType : ObjectType<CatalogingSource>
{
    protected override void Configure(IObjectTypeDescriptor<CatalogingSource> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.TranscribingAgency);
    }
}
