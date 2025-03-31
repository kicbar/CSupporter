using CSupporter.API.Domain.Entities;
using CSupporter.API.Domain.Enums;

namespace CSupporter.API.Domain.Interfaces.IRepositories;

public interface IRoleRepository
{
    Task<Role> GetRole(RoleType roleType, CancellationToken cancellationToken);
}
