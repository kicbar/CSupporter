using MediatR;
using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Application.CQRS.Dictionary.Queries;
using RKAnchor.Server.Application.Models;

namespace RKAnchor.Server.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DictionaryController(IMediator mediator) : ApiControllerBase(mediator)
    {
        [HttpGet("{dictionaryType}")]
        public ActionResult<ApiResult<IEnumerable<string>>> GetDictionary(string dictionaryType)
        {
            var result = _mediator.Send(new GetDictionaryQuery() { DictionaryType = dictionaryType });

            return Success(result.Result);
        }
    }
}
