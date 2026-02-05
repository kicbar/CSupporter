using CSupporter.Application.CQRS.Clients.Command;
using CSupporter.Application.CQRS.Clients.Commands;
using CSupporter.Application.CQRS.Clients.Queries;
using CSupporter.Application.Models;
using CSupporter.Application.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSupporter.API.Controllers.v1;

/// <summary>
/// Controller responsible for managing clients.
/// </summary>
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
    [ProducesResponseType(typeof(ApiResult<ClientDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<ClientDto>>> CreateClient([FromBody] CreateClientCommand createClientCommand, CancellationToken cancellationToken)
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
    [ProducesResponseType(typeof(ApiResult<IEnumerable<ClientDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<IEnumerable<ClientDto>>>> GetAllClients(CancellationToken cancellationToken) 
    {
        var response = await _mediator.Send(new GetAllClientsQuery(), cancellationToken);
        return Success(response);
    }

    /// <summary>
    /// Retrieves a client by id.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     GET /api/v1/Client/14
    /// </remarks>
    /// <param name="clientId">Id of the client.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>Client with the specified id.</returns>
    [HttpGet("{clientId}")]
    [ProducesResponseType(typeof(ApiResult<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<ClientDto>>> GetClientById(int clientId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetClientByIdQuery() { ClientId = clientId }, cancellationToken);
        return Success(response);
    }

    /// <summary>
    /// Updates an existing client.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     PUT /api/v1/Client
    ///     {
    ///         "firstName": "Jason",
    ///         "lastName": "Bourne",
    ///         "clientType": "Individual"
    ///     }
    /// </remarks>
    /// <param name="clientId">ID of the client to update.</param>
    /// <param name="command">Updated client data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated client.</returns>
    [HttpPut("{clientId}")]
    [ProducesResponseType(typeof(ApiResult<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<ClientDto>>> UpdateClient(int clientId, [FromBody] UpdateClientCommand command, CancellationToken cancellationToken)
    {
        command.ClientId = clientId;
        var response = await _mediator.Send(command, cancellationToken);
        return Success(response);
    }

    /// <summary>
    /// Deletes a client by ID.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     DELETE /api/v1/Client/5
    /// </remarks>
    /// <param name="clientId">ID of the client to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the client was deleted.</returns>
    [HttpDelete("{clientId}")]
    [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteClient(int clientId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveClientCommand() { ClientId = clientId }, cancellationToken);
        return Success(response);
    }
}
