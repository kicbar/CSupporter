using CSupporter.Application.CQRS.Clients.Command;
using CSupporter.Domain.Enums;
using FluentValidation;

namespace CSupporter.Application.CQRS.Clients.Validators;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.ClientType)
            .NotEmpty()
            .Must(value => Enum.IsDefined(typeof(ClientType), value));
    }
}
