using MediatR;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces;

namespace RKAnchor.Server.Application.CQRS.Clients.Command;

public record CreateClientCommand : IRequest<Client>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ClientType { get; set; }
}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Client>
{
    private readonly IClientRepository _clientRepository;

    public CreateClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client() 
        { 
            FirstName = request.FirstName,
            LastName = request.LastName,
            ClientType = request.ClientType,
        };

        return await _clientRepository.AddClient(client, cancellationToken);
    }
}
