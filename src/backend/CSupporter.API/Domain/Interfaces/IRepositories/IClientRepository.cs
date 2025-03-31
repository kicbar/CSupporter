using CSupporter.API.Domain.Entities;

namespace CSupporter.API.Domain.Interfaces.IRepositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClients(CancellationToken cancellationToken);
    Task<Client?> GetClientByLastName(string lastName, CancellationToken cancellationToken);
    Task<Client> AddClient(Client client, CancellationToken cancellationToken);
}
