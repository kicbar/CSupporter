using CSupporter.Application.CQRS.Products.Commands;
using CSupporter.Application.CQRS.Products.Queries;
using CSupporter.Application.Models;
using CSupporter.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSupporter.API.Controllers.v1;

/// <summary>
/// Controller responsible for managing products.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
public class ProductController(IMediator mediator) : ApiControllerBase(mediator)
{
    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     POST /api/v1/Product
    ///     {
    ///         "name": "Window p.1",
    ///         "description": "Okno testowe",
    ///         "productType": "window",
    ///         "productCode": "WZ1",
    ///     }
    /// </remarks>
    /// <param name="command">Product data to create.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The newly created product.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<Product>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<Product>>> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Created(response);
    }

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     GET /api/v1/Product
    /// </remarks>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of all products.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<Product>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<IEnumerable<Product>>>> GetAllProducts(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductsQuery(), cancellationToken);
        return Success(response);
    }

    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     GET /api/v1/Product/5
    /// </remarks>
    /// <param name="productId">ID of the product.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product details.</returns>
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(ApiResult<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<Product>>> GetProduct(int productId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductQuery() { ProductId = productId }, cancellationToken);
        return Success(response);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     PUT /api/v1/Product
    ///     {
    ///         "name": "Window p.1",
    ///         "description": "Okno testowe v2",
    ///         "productType": "window",
    ///         "productCode": "WZ2",
    ///     }
    /// </remarks>
    /// <param name="productId">ID of the product to update.</param>
    /// <param name="command">Updated product data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated product.</returns>
    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(ApiResult<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<Product>>> UpdateProduct(int productId, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        command.ProductId = productId;
        var response = await _mediator.Send(command, cancellationToken);
        return Success(response);
    }

    /// <summary>
    /// Deletes a product by ID.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     DELETE /api/v1/Product/5
    /// </remarks>
    /// <param name="productId">ID of the product to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the product was deleted.</returns>
    [HttpDelete("{productId}")]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteProduct(int productId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveProductCommand() { ProductId = productId }, cancellationToken);
        return Success(response);
    }
}
