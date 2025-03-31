using MediatR;
using Microsoft.AspNetCore.Mvc;
using CSupporter.API.Application.CQRS.Products.Commands;
using CSupporter.API.Application.CQRS.Products.Queries;
using CSupporter.API.Application.Filters;
using CSupporter.API.Application.Models;
using CSupporter.API.Domain.Entities;

namespace CSupporter.API.Controllers.v1;

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

    [HttpDelete("{productId}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteProduct(int productId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveProductCommand() { ProductId = productId }, cancellationToken);
        return Success(response);
    }
}
