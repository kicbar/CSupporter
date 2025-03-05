using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Domain.Interfaces;

public interface IUserRepository
{
    Task<int> CreateUser(User user, CancellationToken cancellationToken);
}
