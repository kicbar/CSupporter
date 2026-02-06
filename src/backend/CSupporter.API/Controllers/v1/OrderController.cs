using CSupporter.Application.CQRS.Orders.Commands;
using CSupporter.Application.CQRS.Orders.Queries;
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
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<OrderWithAuditDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<IEnumerable<OrderWithAuditDto>>>> GetAllOrders(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);
        return Created(response);
    }

    [HttpGet("clients/{clientId}")]
    [ProducesResponseType(typeof(ApiResult<OrderWithAuditDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<IEnumerable<OrderForClientDto>>>> GetAllOrders(int clientId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllOrdersForClientQuery() { ClientId = clientId }, cancellationToken);
        return Created(response);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     POST /api/v1/Order/clients/1
    ///     {
    ///       "orderNo": "26/4332",
    ///       "orderDate": "2026-01-30",
    ///       "producerType": "Vetrex",
    ///       "additionalInfo": "Zamówienie przykładowe"
    ///     }
    /// </remarks>
    /// <param name="clientId">Client id.</param>
    /// <param name="command">Order creation data.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The newly created order.</returns>
    [HttpPost("clients/{clientId}")]
    [ProducesResponseType(typeof(ApiResult<OrderWithAuditDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<OrderWithAuditDto>>> CreateOrder(int clientId, [FromBody] CreateOrderForClientCommand command, CancellationToken cancellationToken)
    {
        command.ClientId = clientId;
        var response = await _mediator.Send(command, cancellationToken);
        return Created(response);
    }
}
