using CSupporter.Application.CQRS.Dictionary.Queries;
using CSupporter.Application.Models;
using CSupporter.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSupporter.API.Controllers.v1;

/// <summary>
/// Provides access to predefined dictionary data used across the application (e.g., enums, static values).
/// </summary>
[ApiVersion("1.0")]
public class DictionaryController(IMediator mediator) : ApiControllerBase(mediator)
{
    /// <summary>
    /// Retrieves dictionary values for a given dictionary type
    /// </summary>
    /// <remarks>
    /// **Example Request:**
    /// 
    ///     GET /api/v1/Dictionary/Product
    /// 
    /// Possible values for `dictionaryType`:
    /// - Product
    /// - Client
    /// </remarks>
    /// <param name="dictionaryType">Type of dictionary to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>A list of string values associated with the specified dictionary type.</returns>
    [ResponseCache(Duration = 1200, VaryByQueryKeys = ["name"])]
    [HttpGet("{dictionaryType}")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<ApiResult<IEnumerable<string>>> GetDictionary(DictionaryType dictionaryType, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(new GetDictionaryQuery() { DictionaryType = dictionaryType }, cancellationToken);

        return Success(result.Result);
    }
}
