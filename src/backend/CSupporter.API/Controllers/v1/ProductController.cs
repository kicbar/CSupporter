using CSupporter.Application.CQRS.Products.Commands;
using CSupporter.Application.CQRS.Products.Queries;
using CSupporter.Application.Models;
using CSupporter.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSupporter.API.Controllers.v1;

[ApiVersion("1.0")]
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
