using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using CSupporter.API.Application.Exceptions;
using CSupporter.API.Domain.Entities;
using CSupporter.API.Domain.Enums;
using CSupporter.API.Domain.Interfaces.IRepositories;

namespace CSupporter.API.Application.CQRS.Users.Commands;

public record CreateUserCommand : IRequest<int>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nationality { get; set; }
    public string PasswordHash { get; set; }
}

public class CreateUserCommandCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public CreateUserCommandCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHasher<User> passwordHasher, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userExist = await _userRepository.GetUserByEmail(request.Email, cancellationToken);

        if (userExist is not null) 
            throw new EntityAlreadyExistException(request.Email, userExist.GetType().Name);

        var user = _mapper.Map<User>(request);
        user.Role = await _roleRepository.GetRole(RoleType.User, cancellationToken);

        var passwordHash = _passwordHasher.HashPassword(user, request.PasswordHash);
        user.PasswordHash = passwordHash;

        var result = await _userRepository.CreateUser(user, cancellationToken);

        if (result == 0)
            throw new Exception($"Error during creating user with Email: {request.Email}");
     
        return result;
    }
}
