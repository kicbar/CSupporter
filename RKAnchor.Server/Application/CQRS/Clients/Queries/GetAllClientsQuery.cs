﻿using MediatR;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces.IRepositories;

namespace RKAnchor.Server.Application.CQRS.Clients.Queries;

public class GetAllClientsQuery : IRequest<IEnumerable<Client>> { }

public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<Client>>
{
    private readonly IClientRepository _clientRepository;

    public GetAllClientsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Client>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        return await _clientRepository.GetAllClients(cancellationToken);
    }
}
