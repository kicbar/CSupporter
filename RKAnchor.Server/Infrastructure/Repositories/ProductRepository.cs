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

    public IEnumerable<Product> GetAllProducts()
    {
        return _anchorDbContext.Products;
    }
}
