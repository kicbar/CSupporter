using CSupporter.API.Infrastructure.Data;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.API.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AnchorDbContext _anchorDbContext;

    public UserRepository(AnchorDbContext anchorDbContext)
    {
        _anchorDbContext = anchorDbContext;
    }

    public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return await _anchorDbContext.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
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
