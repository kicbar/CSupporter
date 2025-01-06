using Microsoft.EntityFrameworkCore;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces;
using RKAnchor.Server.Infrastructure.Data;

namespace RKAnchor.Server.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AnchorDbContext _anchorDbContext;

    public ProductRepository(AnchorDbContext anchorDbContext)
    {
        _anchorDbContext  = anchorDbContext;
    }

    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken)
    {
        return await _anchorDbContext.Products.ToListAsync(cancellationToken);
    }
}
