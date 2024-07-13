using Bogus;
using Kathanika.Core.Application.Features.Members.Commands;
using Kathanika.Core.Domain.Aggregates.MemberAggregate;

namespace Kathanika.Core.Application.Test.Features.Members.Commands;

public class CreateMemberCommandHandlerTests
{
    private readonly IMemberRepository memberRepository = Substitute.For<IMemberRepository>();

    [Fact]
    public async Task Handler_ShouldCallRepositoryAddAsyncAndReturnSavedMember_WhenCalled()
    {
        CreateMemberCommand command = new Faker<CreateMemberCommand>()
            .CustomInstantiator(factoryMethod => new CreateMemberCommand(
                factoryMethod.Name.FirstName(),
                factoryMethod.Name.LastName(),
                Guid.NewGuid().ToString(),
                factoryMethod.Date.PastDateOnly(),
                factoryMethod.Address.Country(),
                factoryMethod.Phone.PhoneNumber(),
                factoryMethod.Internet.Email()
            ));
        Member member = Member.Create(
            command.FirstName,
            command.LastName,
            command.PhotoFileId,
            command.DateOfBirth,
            command.Address,
            command.ContactNumber,
            command.Email
        );
        memberRepository.AddAsync(Arg.Any<Member>(), Arg.Any<CancellationToken>())
            .Returns(member);
        CreateMemberCommandHandler handler = new(memberRepository);

        await handler.Handle(command, CancellationToken.None);

        await memberRepository.Received(1)
            .AddAsync(Arg.Is<Member>(member => member.FirstName == command.FirstName), Arg.Any<CancellationToken>());
    }
}
