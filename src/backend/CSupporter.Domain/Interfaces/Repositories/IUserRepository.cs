using CSupporter.Domain.Entities;

namespace CSupporter.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<int> CreateUser(User user, CancellationToken cancellationToken);
}
