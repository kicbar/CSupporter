using CSupporter.API.Infrastructure.Data;
using CSupporter.Application.Exceptions;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.API.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AnchorDbContext _anchorDbContext;

    public ProductRepository(AnchorDbContext anchorDbContext)
    {
        _anchorDbContext = anchorDbContext;
    }

    public async Task<Product> GetProductById(int productId, CancellationToken cancellationToken)
    {
        var product = await _anchorDbContext.Products.FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);
        if (product is null)
            throw new EntityNotFoundException(productId.ToString(), nameof(Product));

        return product;
    }

    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken)
    {
        return await _anchorDbContext.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product> AddProduct(Product product, CancellationToken cancellationToken)
    {
        await _anchorDbContext.Products.AddAsync(product);
        await _anchorDbContext.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        _anchorDbContext.Products.Update(product);
        await _anchorDbContext.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<bool> RemoveProduct(Product product, CancellationToken cancellationToken)
    {
        _anchorDbContext.Products.Remove(product);
        await _anchorDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
