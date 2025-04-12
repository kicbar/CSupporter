using CSupporter.Application.CQRS.Users.Commands;
using CSupporter.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSupporter.API.Controllers.v1;

/// <summary>
/// User account actions.
/// </summary>
[ApiVersion("1.0")]
public class UserController(IMediator mediator) : ApiControllerBase(mediator)
{
    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     POST /api/v1/User/register
    ///     {
    ///         "email": "john.doe@example.com",
    ///         "firstName": "John",
    ///         "lastName": "Doe",
    ///         "nationality": "Polish",
    ///         "passwordHash": "Secret123!"
    ///     }
    /// </remarks>
    /// <param name="command">The user registration data.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>ID of the newly created user.</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResult<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResult<int>>> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Success(result);
    }

    /// <summary>
    /// Logs in a user and returns a JWT token if credentials are valid.
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     POST /api/v1/User/login
    ///     {
    ///         "email": "john.doe@example.com",
    ///         "password": "Secret123!"
    ///     }
    /// </remarks>
    /// <param name="command">Login credentials (username and password).</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>JWT token string.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResult<string>>> LoginUser([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Success(result);
    }
}
