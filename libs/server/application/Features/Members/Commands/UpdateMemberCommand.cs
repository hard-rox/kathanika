namespace Kathanika.Application.Features.Members.Commands;

public sealed record UpdateMemberCommand : IRequest<Member>
{
    public string Id { get; init; }
    public MemberPatch Patch { get; init; }

    public UpdateMemberCommand(string id, MemberPatch patch)
    {
        Id = id;
        Patch = patch;
    }

    public sealed record MemberPatch(
       string? FirstName = null,
       string? LastName = null,
       DateOnly? DateOfBirth = null,
       string? Address = null,
       string? ContactNumber = null,
       string? Email = null
   );
}
