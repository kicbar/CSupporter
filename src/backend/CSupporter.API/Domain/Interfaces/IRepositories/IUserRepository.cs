using CSupporter.API.Domain.Entities;

namespace CSupporter.API.Domain.Interfaces.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<int> CreateUser(User user, CancellationToken cancellationToken);
}
