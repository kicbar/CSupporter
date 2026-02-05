using AutoMapper;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;

namespace CSupporter.Application.CQRS.Clients.Queries;

public class GetAllClientsQuery : IRequest<IEnumerable<ClientWithAuditDto>> { }

internal class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientWithAuditDto>>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public GetAllClientsQueryHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<ClientWithAuditDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.GetAllClients(cancellationToken);

        return _mapper.Map<IEnumerable<ClientWithAuditDto>>(clients);
    }
}
