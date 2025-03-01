namespace RKAnchor.Server.Application.Models;

public class ApiResult<T>
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public ApiResult(int statusCode, bool isSuccess, T? data, string? message = null)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    public static ApiResult<T> Success(T data, string? message = null, int statusCode = StatusCodes.Status200OK)
    {
        return new ApiResult<T>(statusCode, true, data, message);
    }

    public static ApiResult<T> Error(T data, string message, int statusCode = StatusCodes.Status400BadRequest)
    {
        return new ApiResult<T>(statusCode, false, data, message);
    }
}
