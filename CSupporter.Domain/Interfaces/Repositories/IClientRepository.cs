using CSupporter.Domain.Entities;

namespace CSupporter.Domain.Interfaces.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClients(CancellationToken cancellationToken);
    Task<Client?> GetClientByLastName(string lastName, CancellationToken cancellationToken);
    Task<Client> AddClient(Client client, CancellationToken cancellationToken);
}
