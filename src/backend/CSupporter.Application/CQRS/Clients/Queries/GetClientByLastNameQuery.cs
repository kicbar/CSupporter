using AutoMapper;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;

namespace CSupporter.Application.CQRS.Clients.Queries;

public record GetClientByLastNameQuery : IRequest<ClientDto>
{
    public string LastName { get; set; }
}

internal class GetClientByLastNameQueryHandler : IRequestHandler<GetClientByLastNameQuery, ClientDto>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public GetClientByLastNameQueryHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<ClientDto?> Handle(GetClientByLastNameQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientByLastName(request.LastName, cancellationToken);

        return _mapper.Map<ClientDto>(client);
    }
}
