using CSupporter.Application.CQRS.Clients.Queries;
using FluentValidation;

namespace CSupporter.Application.CQRS.Clients.Validators;

public class GetClientByLastNameQueryValidator : AbstractValidator<GetClientByLastNameQuery>
{
    public GetClientByLastNameQueryValidator()
    {
        RuleFor(x => x.LastName)
            .NotEmpty();
    }
}
