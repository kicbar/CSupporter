using Microsoft.EntityFrameworkCore;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Enums;
using RKAnchor.Server.Domain.Interfaces.IRepositories;
using RKAnchor.Server.Infrastructure.Data;

namespace RKAnchor.Server.Infrastructure.Repositories;

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
