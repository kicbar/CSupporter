using CSupporter.Application.CQRS.Products.Commands;
using CSupporter.Domain.Enums;
using FluentValidation;

namespace CSupporter.Application.CQRS.Products.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.ProductType)
            .NotEmpty()
            .Must(value => Enum.IsDefined(typeof(ProductType), value));
    }
}
