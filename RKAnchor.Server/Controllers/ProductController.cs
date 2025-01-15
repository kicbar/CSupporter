using MediatR;
using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Application.CQRS.Product.Commands;
using RKAnchor.Server.Application.CQRS.Product.Queries;
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

    [HttpPost]
    public async Task<Product> CreateProduct([FromBody] CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _mediator.Send(new GetProductsQuery());
    }

    [HttpGet("{productId}")]
    public async Task<Product> GetProduct(int productId)
    {
        return await _mediator.Send(new GetProductQuery() { ProductId = productId });
    }

    [HttpPut("{productId}")]
    public async Task<Product> UpdateProduct(int productId, [FromBody] UpdateProductCommand command)
    {
        command.ProductId = productId;
        return await _mediator.Send(command);
    }

    [HttpDelete("{productId}")]
    public async Task<bool> RemoveProduct(int productId)
    {
        return await _mediator.Send(new RemoveProductCommand() { ProductId = productId });
    }
}
