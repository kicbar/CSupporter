using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;

namespace CSupporter.Application.CQRS.Clients.Queries;

public record GetClientByLastNameQuery : IRequest<Client>
{
    public string LastName { get; set; }
}

public class GetClientByLastNameQueryHandler : IRequestHandler<GetClientByLastNameQuery, Client>
{
    private readonly IClientRepository _clientRepository;

    public GetClientByLastNameQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client?> Handle(GetClientByLastNameQuery request, CancellationToken cancellationToken)
    {
        return await _clientRepository.GetClientByLastName(request.LastName, cancellationToken);
    }
}
