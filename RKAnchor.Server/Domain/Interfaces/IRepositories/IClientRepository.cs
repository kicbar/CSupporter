using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Domain.Interfaces.IRepositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClients(CancellationToken cancellationToken);
    Task<Client?> GetClientByLastName(string lastName, CancellationToken cancellationToken);
    Task<Client> AddClient(Client client, CancellationToken cancellationToken);
}
