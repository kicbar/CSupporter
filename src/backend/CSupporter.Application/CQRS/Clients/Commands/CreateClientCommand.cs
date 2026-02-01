using AutoMapper;
using CSupporter.Application.Converters;
using CSupporter.Application.Interfaces;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;
using System.Text.Json.Serialization;

namespace CSupporter.Application.CQRS.Clients.Command;

public record CreateClientCommand : IRequest<Client>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonConverter(typeof(EnumConverter<ClientType>))]
    public ClientType? ClientType { get; set; }
}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Client>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateClientCommandHandler(IMapper mapper, IClientRepository clientRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService?.UserEmail;
        var client = _mapper.Map<Client>(request);
        client.InsertUser = currentUser ?? "DefaultUser";
        client.UpdateUser = currentUser ?? "DefaultUser";
        client.InsertDate = DateTime.UtcNow;
        client.UpdateDate = DateTime.UtcNow;

        return await _clientRepository.AddClient(client, cancellationToken);
    }
}
