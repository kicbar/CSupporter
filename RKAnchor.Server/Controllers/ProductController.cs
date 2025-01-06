using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces;

namespace RKAnchor.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public IEnumerable<Product> GetAllProducts()
    {
        return _productRepository.GetAllProducts();
    }
}
