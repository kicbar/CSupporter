using CSupporter.Application.CQRS.Products.Commands;
using CSupporter.Domain.Enums;
using FluentValidation;

namespace CSupporter.Application.CQRS.Products.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.Description)
            .MaximumLength(256);

        RuleFor(x => x.ProductCode)
            .MaximumLength(16);

        RuleFor(x => x.ProductType)
            .NotEmpty()
            .Must(value => Enum.IsDefined(typeof(ProductType), value));
    }
}
