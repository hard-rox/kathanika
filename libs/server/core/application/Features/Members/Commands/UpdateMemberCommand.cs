namespace Kathanika.Core.Application.Features.Members.Commands;

public sealed record UpdateMemberCommand(string Id, MemberPatch Patch) : IRequest<Member>;

public sealed record MemberPatch(string? FirstName = null,
                                 string? LastName = null,
                                 DateOnly? DateOfBirth = null,
                                 string? Address = null,
                                 string? ContactNumber = null,
                                 string? Email = null);
