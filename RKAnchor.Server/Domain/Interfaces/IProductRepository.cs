using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductById(int productId, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken);
    Task<Product> AddProduct(Product product, CancellationToken cancellationToken);
    Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken);
    Task<bool> RemoveProduct(Product product, CancellationToken cancellationToken);
}
