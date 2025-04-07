using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;

namespace CSupporter.Domain.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<Role> GetRole(RoleType roleType, CancellationToken cancellationToken);
}
