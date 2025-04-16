using CSupporter.Application.Exceptions;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.API.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CsupporterDbContext _csupporterDbContext;

    public ProductRepository(CsupporterDbContext csupporterDbContext)
    {
        _csupporterDbContext = csupporterDbContext;
    }

    public async Task<Product> GetProductById(int productId, CancellationToken cancellationToken)
    {
        var product = await _csupporterDbContext.Products.FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);
        return product is null ? throw new EntityNotFoundException(productId.ToString(), nameof(Product)) : product;
    }

    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken)
    {
        return await _csupporterDbContext.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product> AddProduct(Product product, CancellationToken cancellationToken)
    {
        await _csupporterDbContext.Products.AddAsync(product);
        await _csupporterDbContext.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        _csupporterDbContext.Products.Update(product);
        await _csupporterDbContext.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<bool> RemoveProduct(Product product, CancellationToken cancellationToken)
    {
        _csupporterDbContext.Products.Remove(product);
        await _csupporterDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
