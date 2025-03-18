using MediatR;
using Microsoft.AspNetCore.Identity;
using RKAnchor.Server.Application.Exceptions;
using RKAnchor.Server.Application.Services;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces.IRepositories;
using RKAnchor.Server.Domain.Interfaces.IServices;

namespace RKAnchor.Server.Application.CQRS.Users.Commands;

public record LoginUserCommand : IRequest<string>
{
    public string Email { get; set; }

    public string Password { get; set; }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtProviderService _jwtProviderService;

    public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtProviderService jwtProviderService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProviderService = jwtProviderService;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.Email, cancellationToken);

        if (user is null)
            throw new EntityNotFoundException(request.Email, user.GetType().Name);

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            throw new Exception("Incorrect user login or password!");

        return _jwtProviderService.GenerateJwtToken(user);
    }
}
