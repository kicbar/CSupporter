using Microsoft.EntityFrameworkCore;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces.IRepositories;
using RKAnchor.Server.Infrastructure.Data;

namespace RKAnchor.Server.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AnchorDbContext _dbContext;

    public ClientRepository(AnchorDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken cancellationToken)
    {
        return await _dbContext.Clients.ToListAsync(cancellationToken);
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
}
