using CSupporter.Application.CQRS.Products.Queries;
using FluentValidation;

namespace CSupporter.Application.CQRS.Products.Validators;

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than 0.");
    }
}
