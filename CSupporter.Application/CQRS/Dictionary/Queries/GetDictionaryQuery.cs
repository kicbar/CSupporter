using CSupporter.Domain.Enums;
using MediatR;

namespace CSupporter.Application.CQRS.Dictionary.Queries;

public record GetDictionaryQuery : IRequest<IEnumerable<string>>
{
    public DictionaryType DictionaryType { get; set; }
}

public class GetDictionaryQueryHandler : IRequestHandler<GetDictionaryQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetDictionaryQuery request, CancellationToken cancellationToken)
    {
        var dictionaryType = request.DictionaryType switch
        {
            DictionaryType.Product => typeof(ProductType),
            DictionaryType.Client => typeof(ClientType),
            _ => throw new ArgumentNullException("Dictionary not exist!")
        };

        var values = Enum.GetValues(dictionaryType)
                 .Cast<Enum>()
                 .Select(e => e.ToString())
                 .ToList();

        if (!values.Any())
            throw new ArgumentNullException("Dictionary not exist!");

        return values;
    }
}
