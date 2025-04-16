using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AnchorDbContext _anchorDbContext;

    public RoleRepository(AnchorDbContext anchorDbContext)
    {
        _anchorDbContext = anchorDbContext;
    }

    public async Task<Role> GetRole(RoleType roleType, CancellationToken cancellationToken)
    {
        return await _anchorDbContext.Roles.FirstOrDefaultAsync(x => x.RoleName == roleType.ToString(), cancellationToken)
            ?? throw new Exception($"Role with name: {roleType} not exist!");
    }
}
