using MediatR;
using Microsoft.AspNetCore.Mvc;
using CSupporter.API.Application.CQRS.Dictionary.Queries;
using CSupporter.API.Application.Models;
using CSupporter.API.Domain.Enums;

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
