using CSupporter.Domain.Entities;

namespace CSupporter.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> GetProductById(int productId, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken);
    Task<Product> AddProduct(Product product, CancellationToken cancellationToken);
    Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken);
    Task<bool> RemoveProduct(Product product, CancellationToken cancellationToken);
}
