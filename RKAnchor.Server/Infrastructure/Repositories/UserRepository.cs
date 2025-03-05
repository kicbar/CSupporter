using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces;
using RKAnchor.Server.Infrastructure.Data;

namespace RKAnchor.Server.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AnchorDbContext _anchorDbContext;

    public UserRepository(AnchorDbContext anchorDbContext)
    {
        _anchorDbContext = anchorDbContext;
    }

    public async Task<int> CreateUser(User user, CancellationToken cancellationToken)
    {
        try
        {
            await _anchorDbContext.AddAsync(user, cancellationToken);
            await _anchorDbContext.SaveChangesAsync();

            return user.Id;
        }
        catch (Exception ex) 
        {
            throw new Exception($"Error while user with email: {user.Email} saving, message: {ex.Message}");
        }
    }
}
