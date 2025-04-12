using CSupporter.Application.CQRS.Products.Commands;
using CSupporter.Domain.Enums;
using FluentValidation;

namespace CSupporter.Application.CQRS.Products.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("ProductId must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.ProductType)
            .NotEmpty()
            .Must(value => Enum.IsDefined(typeof(ProductType), value));
    }
}
