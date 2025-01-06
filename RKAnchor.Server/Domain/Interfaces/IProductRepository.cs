using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken);
}
