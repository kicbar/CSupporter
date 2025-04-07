using CSupporter.Application.CQRS.Dictionary.Queries;
using CSupporter.Application.Models;
using CSupporter.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSupporter.API.Controllers.v1;

[ApiVersion("1.0")]
public class DictionaryController(IMediator mediator) : ApiControllerBase(mediator)
{
    [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "name" })]
    [HttpGet("{dictionaryType}")]
    public ActionResult<ApiResult<IEnumerable<string>>> GetDictionary(DictionaryType dictionaryType, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(new GetDictionaryQuery() { DictionaryType = dictionaryType }, cancellationToken);

        return Success(result.Result);
    }
}
