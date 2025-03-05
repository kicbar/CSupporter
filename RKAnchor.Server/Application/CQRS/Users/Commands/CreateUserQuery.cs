using MediatR;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Enums;
using RKAnchor.Server.Domain.Interfaces;

namespace RKAnchor.Server.Application.CQRS.Users.Commands;

public record CreateUserQuery : IRequest<int>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nationality { get; set; }
    public string PasswordHash { get; set; }
}

public class CreateUserQueryCommandHandler : IRequestHandler<CreateUserQuery, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public CreateUserQueryCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<int> Handle(CreateUserQuery request, CancellationToken cancellationToken)
    {
        var user = new User() 
        { 
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Nationality = request.Nationality,
            PasswordHash = request.PasswordHash,
            Role = await _roleRepository.GetRole(RoleType.User, cancellationToken),
        };
        var result = await _userRepository.CreateUser(user, cancellationToken);

        if (result == 0)
            throw new Exception($"Error during creating user with Email: {request.Email}");
        return result;
    }
}
