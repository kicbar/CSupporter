using MediatR;
using CSupporter.API.Domain.Entities;
using CSupporter.API.Domain.Interfaces.IRepositories;

namespace CSupporter.API.Application.CQRS.Clients.Queries;

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
