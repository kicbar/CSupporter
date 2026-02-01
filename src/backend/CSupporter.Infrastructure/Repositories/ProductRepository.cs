using CSupporter.Application.Exceptions;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.API.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CsupporterDbContext _dbContext;

    public ProductRepository(CsupporterDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetProductById(int productId, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);
        return product is null ? throw new EntityNotFoundException(productId.ToString(), nameof(Product)) : product;
    }

    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken)
    {
        return await _dbContext.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product> AddProduct(Product product, CancellationToken cancellationToken)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<bool> RemoveProduct(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
