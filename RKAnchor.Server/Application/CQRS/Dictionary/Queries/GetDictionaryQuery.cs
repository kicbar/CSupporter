using MediatR;
using RKAnchor.Server.Domain.Enums;

namespace RKAnchor.Server.Application.CQRS.Dictionary.Queries;

public record GetDictionaryQuery : IRequest<IEnumerable<string>>
{
    public string DictionaryType { get; set; }
}

public class GetDictionaryQueryHandler : IRequestHandler<GetDictionaryQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetDictionaryQuery request, CancellationToken cancellationToken)
    {
        Type type = request.DictionaryType.ToLower() switch
        {
            "product" => typeof(ProductType),
            "client" => typeof(ClientType),
            _ => throw new ArgumentNullException("Dictionary not exist!")
        };

        var values = Enum.GetValues(type)
                 .Cast<Enum>()
                 .Select(e => e.ToString())
                 .ToList();

        if (!values.Any())
            throw new ArgumentNullException("Dictionary not exist!");

        return values;
    }
}
