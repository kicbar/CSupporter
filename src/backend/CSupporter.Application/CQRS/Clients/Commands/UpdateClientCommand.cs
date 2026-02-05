using AutoMapper;
using CSupporter.Application.Converters;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;
using System.Text.Json.Serialization;

namespace CSupporter.Application.CQRS.Clients.Commands;

public record UpdateClientCommand : IRequest<ClientDto>
{
    public int ClientId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    [JsonConverter(typeof(EnumConverter<ClientType>))]
    public ClientType? ClientType { get; set; }
}

internal class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientDto>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<ClientDto> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientById(command.ClientId, cancellationToken);
        client.FirstName = command.FirstName;
        client.LastName = command.LastName;
        if (command.ClientType is not null)
            client.ClientType = (ClientType)command.ClientType;

        var updatedClient = await _clientRepository.UpdateClient(client, cancellationToken);

        return _mapper.Map<ClientDto>(updatedClient);
    }
}
