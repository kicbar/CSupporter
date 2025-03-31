using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Domain.Interfaces.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<int> CreateUser(User user, CancellationToken cancellationToken);
}
