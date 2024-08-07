using Kathanika.Core.Application.Features.Members.Commands;
using Kathanika.Core.Domain.Aggregates.MemberAggregate;

namespace Kathanika.Core.Application.Test.Features.Members.Commands;

public class CancelMembershipCommandHandlerTests
{
    private readonly IMemberRepository _memberRepository;
    private readonly CancelMembershipCommandHandler _handler;

    public CancelMembershipCommandHandlerTests()
    {
        _memberRepository = Substitute.For<IMemberRepository>();
        _handler = new CancelMembershipCommandHandler(_memberRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenMemberNotFound()
    {
        // Arrange
        CancelMembershipCommand command = new(Guid.NewGuid().ToString());
        _memberRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(null));

        // Act
        Result<Member> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.NotFound(command.Id), result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenCancellationIsSuccessful()
    {
        // Arrange
        Member dummy = new Faker<Member>()
            .CustomInstantiator(factoryMethod => Member.Create(
                factoryMethod.Name.FirstName(),
                factoryMethod.Name.LastName(),
                Guid.NewGuid().ToString(),
                factoryMethod.Date.PastDateOnly(),
                factoryMethod.Address.FullAddress(),
                factoryMethod.Phone.PhoneNumber(),
                factoryMethod.Internet.Email()
            ).Value);
        CancelMembershipCommand command = new(Guid.NewGuid().ToString());
        _memberRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(dummy));

        // Act
        Result<Member> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(MembershipStatus.Cancelled, result.Value.Status);
        await _memberRepository.Received(1).UpdateAsync(dummy, Arg.Any<CancellationToken>());
    }
}
