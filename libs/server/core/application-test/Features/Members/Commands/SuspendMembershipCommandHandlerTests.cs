using Kathanika.Core.Application.Features.Members.Commands;
using Kathanika.Core.Domain.Aggregates.MemberAggregate;

namespace Kathanika.Core.Application.Test.Features.Members.Commands;

public class SuspendMembershipCommandHandlerTests
{
    private readonly IMemberRepository _memberRepository;
    private readonly SuspendMembershipCommandHandler _handler;
    private readonly Faker<Member> _memberFaker;

    public SuspendMembershipCommandHandlerTests()
    {
        _memberRepository = Substitute.For<IMemberRepository>();
        _handler = new SuspendMembershipCommandHandler(_memberRepository);
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
        SuspendMembershipCommand command = new(Guid.NewGuid().ToString());
        _memberRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(null));

        // Act
        Result<Member> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.NotFound(command.Id), result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenSuspendMembershipSucceeds()
    {
        // Arrange
        Member member = _memberFaker.Generate();
        _memberRepository.GetByIdAsync(member.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(member));

        SuspendMembershipCommand command = new(member.Id);

        // Act
        Result<Member> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(member.Id, result.Value.Id);
        await _memberRepository.Received().UpdateAsync(member, Arg.Any<CancellationToken>());
    }
}
