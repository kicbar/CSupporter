using CSupporter.Application.CQRS.Dictionary.Queries;
using CSupporter.Domain.Enums;
using FluentValidation;

namespace CSupporter.Application.CQRS.Dictionary.Validators;

public class GetDictionaryQueryValidator : AbstractValidator<GetDictionaryQuery>
{
    public GetDictionaryQueryValidator()
    {
        RuleFor(x => x.DictionaryType)
            .NotEmpty()
            .Must(value => Enum.IsDefined(typeof(DictionaryType), value));
    }
}
