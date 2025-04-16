using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CsupporterDbContext _csupporterDbContext;

    public UserRepository(CsupporterDbContext csupporterDbContext)
    {
        _csupporterDbContext = csupporterDbContext;
    }

    public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return await _csupporterDbContext.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<int> CreateUser(User user, CancellationToken cancellationToken)
    {
        try
        {
            await _csupporterDbContext.AddAsync(user, cancellationToken);
            await _csupporterDbContext.SaveChangesAsync();

            return user.Id;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while user with email: {user.Email} saving, message: {ex.Message}");
        }
    }
}
