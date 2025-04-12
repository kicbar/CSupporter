using CSupporter.Application.CQRS.Products.Commands;
using FluentValidation;

namespace CSupporter.Application.CQRS.Products.Validators;

public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than 0.");
    }
}
