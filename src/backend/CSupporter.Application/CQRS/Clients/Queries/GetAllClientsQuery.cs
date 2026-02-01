using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CSupporter.Application.CQRS.Clients.Queries;

public class GetAllClientsQuery : IRequest<IEnumerable<Client>> { }

public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<Client>>
{
    private readonly ILogger<GetAllClientsQueryHandler> _logger;
    private readonly IClientRepository _clientRepository;

    public GetAllClientsQueryHandler(IClientRepository clientRepository, ILogger<GetAllClientsQueryHandler> logger)
    {
        _logger = logger;
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Client>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Thread: {Thread.CurrentThread.ManagedThreadId}");

        var result = await _clientRepository.GetAllClients(cancellationToken);

        _logger.LogInformation($"Thread: {Thread.CurrentThread.ManagedThreadId}");

        return result;
    }
}
