using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;

namespace Kathanika.Infrastructure.Graphql.GraphqlHelpers;

internal static class IgnoreAuditPropertiesHelper
{
    internal static void IgnoreAuditFieldsFromType<T>(this IObjectTypeDescriptor<T> descriptor) where T : AggregateRoot
    {
        descriptor.Ignore(x => x.CreatedAt);
        descriptor.Ignore(x => x.CreatedByUserId);
        descriptor.Ignore(x => x.CreatedByUserName);
        descriptor.Ignore(x => x.LastModifiedAt);
        descriptor.Ignore(x => x.LastModifiedByUserId);
        descriptor.Ignore(x => x.LastModifiedByUserName);
    }

    internal static void IgnoreAuditFieldsFromFilterInputType<T>(this IFilterInputTypeDescriptor<T> descriptor) where T : AggregateRoot
    {
        descriptor.Ignore(x => x.CreatedAt);
        descriptor.Ignore(x => x.CreatedByUserId);
        descriptor.Ignore(x => x.CreatedByUserName);
        descriptor.Ignore(x => x.LastModifiedAt);
        descriptor.Ignore(x => x.LastModifiedByUserId);
        descriptor.Ignore(x => x.LastModifiedByUserName);
    }

    internal static void IgnoreAuditFieldsFromSortInputType<T>(this ISortInputTypeDescriptor<T> descriptor) where T : AggregateRoot
    {
        descriptor.Ignore(x => x.CreatedAt);
        descriptor.Ignore(x => x.CreatedByUserId);
        descriptor.Ignore(x => x.CreatedByUserName);
        descriptor.Ignore(x => x.LastModifiedAt);
        descriptor.Ignore(x => x.LastModifiedByUserId);
        descriptor.Ignore(x => x.LastModifiedByUserName);
    }
}