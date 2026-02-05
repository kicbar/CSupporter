using AutoMapper;
using CSupporter.Application.Converters;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;
using System.Text.Json.Serialization;

namespace CSupporter.Application.CQRS.Clients.Command;

public record CreateClientCommand : IRequest<ClientDto>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    [JsonConverter(typeof(EnumConverter<ClientType>))]
    public ClientType? ClientType { get; set; }
}

internal class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDto>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public CreateClientCommandHandler(IMapper mapper, IClientRepository clientRepository)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = _mapper.Map<Client>(request);

        var createdClient = await _clientRepository.AddClient(client, cancellationToken);

        return _mapper.Map<ClientDto>(createdClient);
    }
}
