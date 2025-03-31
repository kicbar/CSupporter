using MediatR;
using Microsoft.AspNetCore.Mvc;
using CSupporter.API.Application.Models;

namespace CSupporter.API.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IMediator _mediator;

    public ApiControllerBase(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    protected ActionResult<ApiResult<T>> Success<T>(T data, string message = "") =>
        Success(data, message, StatusCodes.Status200OK);
    protected ActionResult<ApiResult<T>> Created<T>(T data, string message) =>
        Success(data, message, StatusCodes.Status201Created);
    protected ActionResult<ApiResult<T>> Error<T>(T data, string message) =>
        BadRequest(ApiResult<T>.Error(data, message));
    private ActionResult<ApiResult<T>> Success<T>(T? data, string message, int statusCode) =>
        Ok(ApiResult<T>.Success(data, message, statusCode));
}
