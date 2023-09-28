﻿using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Publishers.Commands;

internal sealed class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand>
{
    private readonly IPublisherRepository publisherRepository;
    private readonly IPublicationRepository publicationRepository;

    public DeletePublisherCommandHandler(IPublisherRepository publisherRepository, IPublicationRepository publicationRepository)
    {
        this.publisherRepository = publisherRepository;
        this.publicationRepository = publicationRepository;
    }

    public async Task Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        _ = await publisherRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundWithTheIdException(typeof(Publisher),request.Id);

        ///TODO: var hasPublication = (await publicationRepository.ListAllAsync(x => x.Publisher))
        await publisherRepository.DeleteAsync(request.Id);
    }
}
