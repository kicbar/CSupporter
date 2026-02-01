using CSupporter.Domain.Interfaces.Repositories;
using MediatR;

namespace CSupporter.Application.CQRS.Clients.Commands;

public record RemoveClientCommand : IRequest<bool>
{
    public int ClientId { get; set; }
}

public class RemoveClientCommandHandler : IRequestHandler<RemoveClientCommand, bool>
{
    private IClientRepository _clientRepository;

    public RemoveClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<bool> Handle(RemoveClientCommand command, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientById(command.ClientId, cancellationToken);

        return await _clientRepository.RemoveClient(client, cancellationToken);
    }
}
