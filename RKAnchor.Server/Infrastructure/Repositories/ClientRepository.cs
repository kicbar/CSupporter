using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces;
using RKAnchor.Server.Infrastructure.Data;

namespace RKAnchor.Server.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly string _connectionString;
    private readonly AnchorDbContext _dbContext;

    public ClientRepository(AnchorDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _connectionString = configuration.GetConnectionString("AnchorDbConnection");
    }

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken cancellationToken)
    {
        return await _dbContext.Clients.ToListAsync(cancellationToken);
    }

    public async Task<Client?> GetClientByLastName(string lastName, CancellationToken cancellationToken)
    {
        try
        {
            //SELECT * FROM Clients WHERE Name LIKE '%' OR 1=1; DROP TABLE Clients; -- '%'
            //SELECT * FROM Clients WHERE Name LIKE '%' DELETE FROM Clients; -- '%'
            string query = $"SELECT * FROM Clients WHERE LastName LIKE '%{lastName}%'";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    return new Client
                    {
                        Id = reader.GetInt32(0),
                        LastName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        ClientType = reader.GetString(3),
                    };
                }
            }

            return null;
        }
        catch (Exception exc)
        {
            throw exc; 
        }
    }

    public async Task<Client> AddClient(Client client, CancellationToken cancellationToken)
    {
        await _dbContext.Clients.AddAsync(client);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return client;
    }
}
