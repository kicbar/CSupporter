using Microsoft.EntityFrameworkCore;
using CSupporter.API.Domain.Entities;
using CSupporter.API.Domain.Enums;
using CSupporter.API.Domain.Interfaces.IRepositories;
using CSupporter.API.Infrastructure.Data;

namespace CSupporter.API.Infrastructure.Repositories;

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
