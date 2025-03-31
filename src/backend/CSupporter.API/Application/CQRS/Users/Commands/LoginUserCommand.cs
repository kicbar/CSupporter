﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using CSupporter.API.Application.Exceptions;
using CSupporter.API.Application.Services;
using CSupporter.API.Domain.Entities;
using CSupporter.API.Domain.Interfaces.IRepositories;
using CSupporter.API.Domain.Interfaces.IServices;

namespace CSupporter.API.Application.CQRS.Users.Commands;

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
            throw new EntityNotFoundException(request.Email, nameof(User));

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            throw new Exception("Incorrect user login or password!");

        return _jwtProviderService.GenerateJwtToken(user);
    }
}
