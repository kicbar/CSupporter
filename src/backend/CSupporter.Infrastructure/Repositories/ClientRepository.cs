using CSupporter.Application.Exceptions;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly CsupporterDbContext _dbContext;

    public ClientRepository(CsupporterDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken cancellationToken)
    {
        return await _dbContext.Clients.ToListAsync(cancellationToken);
    }

    public async Task<Client?> GetClientById(int clientId, CancellationToken cancellationToken)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == clientId, cancellationToken);
        return client is null ? throw new EntityNotFoundException(clientId.ToString(), nameof(Client)) : client;
    }

    public async Task<Client?> GetClientByLastName(string lastName, CancellationToken cancellationToken)
    {
        return await _dbContext.Clients.FirstOrDefaultAsync(x => x.LastName.Contains(lastName), cancellationToken);
    }

    public async Task<Client> AddClient(Client client, CancellationToken cancellationToken)
    {
        await _dbContext.Clients.AddAsync(client);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return client;
    }

    public async Task<Client> UpdateClient(Client client, CancellationToken cancellationToken)
    {
        _dbContext.Clients.Update(client);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return client;
    }

    public async Task<bool> RemoveClient(Client client, CancellationToken cancellationToken)
    {
        _dbContext.Clients.Remove(client);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
