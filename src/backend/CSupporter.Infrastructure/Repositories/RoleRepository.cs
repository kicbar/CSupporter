using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly CsupporterDbContext _csupporterDbContext;

    public RoleRepository(CsupporterDbContext csupporterDbContext)
    {
        _csupporterDbContext = csupporterDbContext;
    }

    public async Task<Role> GetRole(RoleType roleType, CancellationToken cancellationToken)
    {
        return await _csupporterDbContext.Roles.FirstOrDefaultAsync(x => x.RoleName == roleType.ToString(), cancellationToken)
            ?? throw new Exception($"Role with name: {roleType} not exist!");
    }
}
