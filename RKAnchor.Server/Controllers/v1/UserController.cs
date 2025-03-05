using MediatR;
using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Application.CQRS.Users.Commands;
using RKAnchor.Server.Application.Models;

namespace RKAnchor.Server.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : ApiControllerBase
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<ActionResult<ApiResult<int>>> CreateUser([FromBody] CreateUserQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Success(result);
        }
    }
}
