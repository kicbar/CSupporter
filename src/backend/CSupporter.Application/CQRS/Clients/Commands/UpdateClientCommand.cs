using AutoMapper;
using CSupporter.Application.Converters;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;
using System.Text.Json.Serialization;

namespace CSupporter.Application.CQRS.Clients.Commands;

public record UpdateClientCommand : IRequest<ClientWithAuditDto>
{
    public int ClientId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    [JsonConverter(typeof(EnumConverter<ClientType>))]
    public ClientType? ClientType { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }
}

internal class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientWithAuditDto>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<ClientWithAuditDto> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientById(command.ClientId, cancellationToken);
        if (command.FirstName is not null) client.FirstName = command.FirstName.Trim();
        if (command.LastName is not null) client.LastName = command.LastName.Trim();
        if (command.PhoneNumber is not null) client.PhoneNumber = command.PhoneNumber.Trim();
        if (command.Address is not null) client.Address = command.Address.Trim();
        if (command.Email is not null) client.Email = command.Email.Trim();
        if (command.ClientType is not null)
            client.ClientType = (ClientType)command.ClientType;

        var updatedClient = await _clientRepository.UpdateClient(client, cancellationToken);

        return _mapper.Map<ClientWithAuditDto>(updatedClient);
    }
}
