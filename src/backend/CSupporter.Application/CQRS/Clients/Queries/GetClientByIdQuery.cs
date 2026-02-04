using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;

namespace CSupporter.Application.CQRS.Clients.Queries;

public record GetClientByIdQuery : IRequest<Client>
{
    public int ClientId { get; set; }
}

internal class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client>
{
    private readonly IClientRepository _clientRepository;

    public GetClientByIdQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        return await _clientRepository.GetClientById(request.ClientId, cancellationToken);
    }
}
