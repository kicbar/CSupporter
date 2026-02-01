using CSupporter.Application.CQRS.Dictionary.Queries;
using FluentValidation;

namespace CSupporter.Application.CQRS.Dictionary.Validators;

public class GetDictionaryQueryValidator : AbstractValidator<GetDictionaryQuery>
{
    public GetDictionaryQueryValidator()
    {
        RuleFor(x => x.DictionaryType)
            .NotNull()
            .IsInEnum();
    }
}
