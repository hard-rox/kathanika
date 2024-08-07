using Kathanika.Core.Application.Features.Members.Queries;
using Kathanika.Core.Domain.Aggregates.MemberAggregate;

namespace Kathanika.Core.Application.Test.Features.Members.Queries;

public class GetMemberByIdQueryHandlerTests
{
    private readonly IMemberRepository _memberRepository;
    private readonly GetMemberByIdQueryHandler _handler;

    public GetMemberByIdQueryHandlerTests()
    {
        _memberRepository = Substitute.For<IMemberRepository>();
        _handler = new GetMemberByIdQueryHandler(_memberRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenMemberNotFound()
    {
        // Arrange
        GetMemberByIdQuery query = new(Guid.NewGuid().ToString());
        _memberRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Member?>(null));

        // Act
        Result<Member> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(MemberAggregateErrors.NotFound(query.Id), result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenMemberFound()
    {
        // Arrange
        string memberId = Guid.NewGuid().ToString();
        Member member = new Faker<Member>()
            .CustomInstantiator(factoryMethod => Member.Create(
                factoryMethod.Name.FirstName(),
                factoryMethod.Name.LastName(),
                string.Empty,
                factoryMethod.Date.PastDateOnly(),
                factoryMethod.Address.FullAddress(),
                factoryMethod.Phone.PhoneNumber(),
                factoryMethod.Internet.Email()
            ).Value);

        GetMemberByIdQuery query = new(memberId);
        _memberRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Member?>(member));

        // Act
        Result<Member> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(member.FirstName, result.Value.FirstName);
        Assert.Equal(member.LastName, result.Value.LastName);
        Assert.Equal(member.Email, result.Value.Email);
    }
}
