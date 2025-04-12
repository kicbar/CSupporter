using CSupporter.Application.CQRS.Users.Commands;
using FluentValidation;

namespace CSupporter.Application.CQRS.Users.Validators;

public class LoginUserCommandValdator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValdator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(64);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(64);
    }
}
