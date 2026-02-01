using CSupporter.Application.CQRS.Clients.Commands;
using FluentValidation;

namespace CSupporter.Application.CQRS.Clients.Validators;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("ClientId must be greater than 0.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.LastName)
            .MaximumLength(64);

        RuleFor(x => x.ClientType)
            .NotNull()
            .IsInEnum();
    }
}