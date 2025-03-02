using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Domain.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetClientByLastName(string lastName, CancellationToken cancellationToken);
}
