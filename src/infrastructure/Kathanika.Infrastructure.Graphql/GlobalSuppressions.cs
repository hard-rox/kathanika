using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.MemberMutations.CancelMembershipAsync(MediatR.IMediator,System.String,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Kathanika.Infrastructure.Graphql.Payloads.CancelMembershipPayload}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.MemberMutations.CreateMemberAsync(MediatR.IMediator,System.Threading.CancellationToken,Kathanika.Application.Features.Members.Commands.CreateMemberCommand)~System.Threading.Tasks.Task{Kathanika.Infrastructure.Graphql.Payloads.CreateMemberPayload}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.MemberMutations.RenewMembershipAsync(MediatR.IMediator,System.String,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Kathanika.Infrastructure.Graphql.Payloads.RenewMembershipPayload}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.MemberMutations.SuspendMembershipAsync(MediatR.IMediator,System.String,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Kathanika.Infrastructure.Graphql.Payloads.SuspendMembershipPayload}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.MemberMutations.UpdateMemberAsync(MediatR.IMediator,Kathanika.Application.Features.Members.Commands.UpdateMemberCommand,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Kathanika.Infrastructure.Graphql.Payloads.UpdateMemberPayload}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.MemberQueries.GetMemberAsync(MediatR.IMediator,HotChocolate.Resolvers.IResolverContext,System.String,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Kathanika.Domain.Aggregates.MemberAggregate.Member}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.MemberQueries.GetMembersAsync(MediatR.IMediator,System.Threading.CancellationToken)~System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{Kathanika.Domain.Aggregates.MemberAggregate.Member}}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.BibRecordMutations.AcquirePublicationAsync(MediatR.IMediator,Kathanika.Application.Features.Publications.Commands.AcquirePublicationCommand,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Kathanika.Infrastructure.Graphql.Payloads.AcquirePublicationPayload}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.BibRecordMutations.UpdatePublicationAsync(MediatR.IMediator,Kathanika.Application.Features.Publications.Commands.UpdatePublicationCommand,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Kathanika.Infrastructure.Graphql.Payloads.UpdatePublicationPayload}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.Queries.GetPublicationAsync(MediatR.IMediator,HotChocolate.Resolvers.IResolverContext,System.String,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Kathanika.Domain.Aggregates.PublicationAggregate.Publication}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.Queries.GetPublicationsAsync(MediatR.IMediator,System.Threading.CancellationToken)~System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{Kathanika.Domain.Aggregates.PublicationAggregate.Publication}}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.Subscriptions.OnNewNotification(Kathanika.Infrastructure.Graphql.Schema.Notification)~Kathanika.Infrastructure.Graphql.Schema.Notification")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.VendorMutations.AddVendorAsync(MediatR.IMediator,System.Threading.CancellationToken,Kathanika.Application.Features.Vendors.Commands.AddVendorCommand)~System.Threading.Tasks.Task{Kathanika.Infrastructure.Graphql.Payloads.AddVendorPayload}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.VendorQueries.GetVendorAsync(MediatR.IMediator,HotChocolate.Resolvers.IResolverContext,System.String,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Kathanika.Domain.Aggregates.VendorAggregate.Vendor}")]
[assembly:
    SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member",
        Target =
            "~M:Kathanika.Infrastructure.Graphql.Schema.VendorQueries.GetVendorsAsync(MediatR.IMediator,System.Threading.CancellationToken)~System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{Kathanika.Domain.Aggregates.VendorAggregate.Vendor}}")]
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.