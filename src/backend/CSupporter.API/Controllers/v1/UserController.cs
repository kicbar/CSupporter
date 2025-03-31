using MediatR;
using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Application.CQRS.Users.Commands;
using RKAnchor.Server.Application.Models;

namespace RKAnchor.Server.Controllers.v1;

[ApiVersion("1.0")]
public class UserController : ApiControllerBase
{
    public UserController(IMediator mediator) : base(mediator) { }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResult<int>>> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Success(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResult<string>>> LoginUser([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Success(result);
    }
}
