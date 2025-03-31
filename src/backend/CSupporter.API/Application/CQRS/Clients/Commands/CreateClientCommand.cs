using AutoMapper;
using MediatR;
using CSupporter.API.Application.Services;
using CSupporter.API.Domain.Entities;
using CSupporter.API.Domain.Interfaces.IRepositories;
using CSupporter.API.Domain.Interfaces.IServices;

namespace CSupporter.API.Application.CQRS.Clients.Command;

public record CreateClientCommand : IRequest<Client>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ClientType { get; set; }
}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Client>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateClientCommandHandler(IMapper mapper, IClientRepository clientRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService?.UserEmail;
        var client = _mapper.Map<Client>(request);
        if (currentUser is not null) 
        {
            client.InsertUser = currentUser;
            client.UpdateUser = currentUser;
        }

        return await _clientRepository.AddClient(client, cancellationToken);
    }
}
