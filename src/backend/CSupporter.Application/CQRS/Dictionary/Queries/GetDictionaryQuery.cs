using CSupporter.Application.Converters;
using CSupporter.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace CSupporter.Application.CQRS.Dictionary.Queries;

public record GetDictionaryQuery : IRequest<IEnumerable<string>>
{
    [JsonConverter(typeof(EnumConverter<DictionaryType>))]
    public DictionaryType DictionaryType { get; set; }
}

internal class GetDictionaryQueryHandler : IRequestHandler<GetDictionaryQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetDictionaryQuery request, CancellationToken cancellationToken)
    {
        var dictionaryType = request.DictionaryType switch
        {
            DictionaryType.Product => typeof(ProductType),
            DictionaryType.Producer => typeof(ProducerType),
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
