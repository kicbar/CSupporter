using CSupporter.Application.CQRS.Orders.Commands;
using CSupporter.Application.Models;
using CSupporter.Application.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSupporter.API.Controllers.v1;

/// <summary>
/// Controller responsible for managing orders.
/// </summary>
[ApiVersion("1.0")]
public class OrderController(IMediator mediator) : ApiControllerBase(mediator)
{
    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     POST /api/v1/Order
    ///     {
    ///       "clientId": 1,
    ///       "orderNo": "26/4332",
    ///       "orderDate": "2026-01-30",
    ///       "producerType": "Vetrex",
    ///       "additionalInfo": "Zamówienie przykładowe"
    ///     }
    /// </remarks>
    /// <param name="command">Order creation data.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The newly created order.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<OrderWithAuditDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<OrderWithAuditDto>>> CreateOrder([FromBody] CreateOrderForClientCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Created(response);
    }
}
