using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Enums;

namespace RKAnchor.Server.Domain.Interfaces;

public interface IRoleRepository
{
    Task<Role> GetRole(RoleType roleType, CancellationToken cancellationToken);
}
