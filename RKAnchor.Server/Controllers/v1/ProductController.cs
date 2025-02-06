using MediatR;
using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Application.CQRS.Product.Commands;
using RKAnchor.Server.Application.CQRS.Product.Queries;
using RKAnchor.Server.Application.Models;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Controllers.v1;

[ApiVersion("1.0")]
public class ProductController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult<Product>>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var response = await _mediator.Send(command);
        return Success(response);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<IEnumerable<Product>>>> GetAllProducts()
    {
        var response = await _mediator.Send(new GetProductsQuery());
        return Success(response);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ApiResult<Product>>> GetProduct(int productId)
    {
        var response = await _mediator.Send(new GetProductQuery() { ProductId = productId });
        return Success(response);

    }

    [HttpPut("{productId}")]
    public async Task<ActionResult<ApiResult<Product>>> UpdateProduct(int productId, [FromBody] UpdateProductCommand command)
    {
        command.ProductId = productId;
        var response = await _mediator.Send(command);
        return Success(response);
    }
}