using MediatR;
using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Application.Product.Queries;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _mediator.Send(new GetProductsQuery());
    }
}
