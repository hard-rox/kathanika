using Kathanika.Core.Application.Features.Members.Commands;
using Kathanika.Core.Domain.Aggregates.MemberAggregate;

namespace Kathanika.Core.Application.Test.Features.Members.Commands;

public class UpdateMemberCommandHandlerTests
{
    private readonly IMemberRepository _memberRepository;
    private readonly UpdateMemberCommandHandler _handler;
    private readonly Faker<Member> _memberFaker;

    public UpdateMemberCommandHandlerTests()
    {
        _memberRepository = Substitute.For<IMemberRepository>();
        _handler = new UpdateMemberCommandHandler(_memberRepository);
        _memberFaker = new Faker<Member>()
            .CustomInstantiator(f => Member.Create(
                f.Name.FirstName(),
                f.Name.LastName(),
                f.Random.Guid().ToString(),
                DateOnly.FromDateTime(f.Date.Past(30, DateTime.Now.AddYears(-18))),
                f.Address.FullAddress(),
                f.Phone.PhoneNumber(),
                f.Internet.Email()).Value);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenMemberNotFound()
    {
        // Arrange
        UpdateMemberCommand command = new(Guid.NewGuid().ToString(), new MemberPatch());
        _memberRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(null));

        // Act
        Result<Member> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.NotFound(command.Id), result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenUpdateMemberSucceeds()
    {
        // Arrange
        Member member = _memberFaker.Generate();
        _memberRepository.GetByIdAsync(member.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(member));

        UpdateMemberCommand command = new(member.Id,
            new MemberPatch
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                PhotoFileId = "UpdatedPhotoFileId",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
                Address = "UpdatedAddress",
                ContactNumber = "UpdatedContactNumber",
                Email = "updated.email@example.com"
            });

        // Act
        Result<Member> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(member.Id, result.Value.Id);
        await _memberRepository.Received().UpdateAsync(member, Arg.Any<CancellationToken>());
    }
}
