using CSupporter.Application.CQRS.Clients.Queries;
using FluentValidation;

namespace CSupporter.Application.CQRS.Clients.Validators;

public class GetClientByIdQueryValidator : AbstractValidator<GetClientByIdQuery>
{
    public GetClientByIdQueryValidator()
    {
        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("ProductId must be greater than 0.");
    }
}