using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Domain.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
}
