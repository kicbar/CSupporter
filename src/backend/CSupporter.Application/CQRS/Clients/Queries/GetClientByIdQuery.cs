using AutoMapper;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;

namespace CSupporter.Application.CQRS.Clients.Queries;

public record GetClientByIdQuery : IRequest<ClientWithAuditDto>
{
    public int ClientId { get; set; }
}

internal class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientWithAuditDto>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public GetClientByIdQueryHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<ClientWithAuditDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientById(request.ClientId, cancellationToken);

        return _mapper.Map<ClientWithAuditDto>(client);
    }
}
