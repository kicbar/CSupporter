using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Application.CQRS.Clients.Command;
using RKAnchor.Server.Application.CQRS.Clients.Queries;
using RKAnchor.Server.Application.Models;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Controllers.v1;

[ApiVersion("1.0")]
public class ClientController : ApiControllerBase
{
    public ClientController(IMediator mediator) : base(mediator) { }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ApiResult<IEnumerable<Client>>>> GetAllClients(CancellationToken cancellationToken) 
    {
        var response = await _mediator.Send(new GetAllClientsQuery(), cancellationToken);
        return Success(response);
    }

    [HttpGet("{lastName}")]
    public async Task<ActionResult<ApiResult<Client>>> GetClientByName(string lastName, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetClientByLastNameQuery() { LastName = lastName }, cancellationToken);
        return Success(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResult<Client>>> CreateClient([FromBody] CreateClientCommand createClientCommand, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(createClientCommand, cancellationToken);
        return Success(response);
    }
}
