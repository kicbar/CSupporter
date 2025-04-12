using CSupporter.Application.CQRS.Clients.Command;
using CSupporter.Application.CQRS.Clients.Queries;
using CSupporter.Application.Models;
using CSupporter.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSupporter.API.Controllers.v1;

/// <summary>
/// Controller responsible for managing clients.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
public class ClientController(IMediator mediator) : ApiControllerBase(mediator)
{
    /// <summary>
    /// Creates a new client.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     POST /api/v1/Client
    ///     {
    ///         "firstName": "John",
    ///         "lastName": "Doe",
    ///         "clientType": "Individual"
    ///     }
    /// </remarks>
    /// <param name="createClientCommand">Client creation data.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The newly created client.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<Client>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<Client>>> CreateClient([FromBody] CreateClientCommand createClientCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createClientCommand, cancellationToken);
        return Created(response);
    }

    /// <summary>
    /// Retrieves a list of all clients.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     GET /api/v1/Client
    /// </remarks>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>List of all clients.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<Client>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<IEnumerable<Client>>>> GetAllClients(CancellationToken cancellationToken) 
    {
        var response = await _mediator.Send(new GetAllClientsQuery(), cancellationToken);
        return Success(response);
    }

    /// <summary>
    /// Retrieves a client by their last name.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     GET /api/v1/Client/Doe
    /// </remarks>
    /// <param name="lastName">Last name of the client.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>Client with the specified last name.</returns>
    [HttpGet("{lastName}")]
    [ProducesResponseType(typeof(ApiResult<Client>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<Client>>> GetClientByName(string lastName, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetClientByLastNameQuery() { LastName = lastName }, cancellationToken);
        return Success(response);
    }
}
