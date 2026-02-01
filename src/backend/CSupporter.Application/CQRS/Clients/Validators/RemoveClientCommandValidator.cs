using CSupporter.Application.CQRS.Clients.Commands;
using CSupporter.Application.CQRS.Products.Commands;
using FluentValidation;

namespace CSupporter.Application.CQRS.Clients.Validators;

public class RemoveClientCommandValidator : AbstractValidator<RemoveClientCommand>
{
    public RemoveClientCommandValidator()
    {
        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("ClientId must be greater than 0.");
    }
}