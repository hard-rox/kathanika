using Kathanika.Core.Application.Features.Members.Commands;
using Kathanika.Core.Domain.Aggregates.MemberAggregate;

namespace Kathanika.Core.Application.Test.Features.Members.Commands;

public class RenewMembershipCommandHandlerTests
{
    private readonly IMemberRepository _memberRepository;
    private readonly RenewMembershipCommandHandler _handler;
    private readonly Faker<Member> _memberFaker;

    public RenewMembershipCommandHandlerTests()
    {
        _memberRepository = Substitute.For<IMemberRepository>();
        _handler = new RenewMembershipCommandHandler(_memberRepository);
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
        RenewMembershipCommand command = new(Guid.NewGuid().ToString());
        _memberRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(null));

        // Act
        Result<Member> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.NotFound(command.Id), result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenRenewMembershipIsActive()
    {
        // Arrange
        Member member = _memberFaker.Generate();
        _memberRepository.GetByIdAsync(member.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(member));

        RenewMembershipCommand command = new(member.Id);

        // Act
        Result<Member> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.ActiveMembership, result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenRenewMembershipSucceeds()
    {
        // Arrange
        Member member = _memberFaker.Generate();
        member.SuspendMembership();
        _memberRepository.GetByIdAsync(member.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(member));

        RenewMembershipCommand command = new(member.Id);

        // Act
        Result<Member> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(member.Id, result.Value.Id);
        await _memberRepository.Received().UpdateAsync(member, Arg.Any<CancellationToken>());
    }
}
