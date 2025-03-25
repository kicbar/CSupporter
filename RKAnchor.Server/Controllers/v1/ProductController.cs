using MediatR;
using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Application.CQRS.Products.Commands;
using RKAnchor.Server.Application.CQRS.Products.Queries;
using RKAnchor.Server.Application.Filters;
using RKAnchor.Server.Application.Models;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Controllers.v1;

[ApiVersion("1.0")]
[TimeTrackFilter]
public class ProductController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost]
    public async Task<ActionResult<ApiResult<Product>>> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Success(response);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<IEnumerable<Product>>>> GetAllProducts(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductsQuery(), cancellationToken);
        return Success(response);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ApiResult<Product>>> GetProduct(int productId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductQuery() { ProductId = productId }, cancellationToken);
        return Success(response);
    }

    [HttpPut("{productId}")]
    public async Task<ActionResult<ApiResult<Product>>> UpdateProduct(int productId, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        command.ProductId = productId;
        var response = await _mediator.Send(command, cancellationToken);
        return Success(response);
    }
}
