using MediatR;
using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Application.CQRS.Clients.Queries;
using RKAnchor.Server.Application.Models;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ApiControllerBase
    {
        public ClientController(IMediator mediator) : base(mediator) { }

        [HttpGet("{lastName}")]
        public async Task<ActionResult<ApiResult<Client>>> GetProduct(string lastName, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetClientByLastNameQuery() { LastName = lastName }, cancellationToken);
            return Success(response);
        }
    }
}
