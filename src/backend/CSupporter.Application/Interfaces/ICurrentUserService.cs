using Microsoft.AspNetCore.Http;

namespace CSupporter.Application.Interfaces;

public interface ICurrentUserService
{
    public string? UserId { get; }
    public string? UserEmail { get; }
    public string? UserName { get; }
    public string? UserRole { get; }
    public IEnumerable<string>? UserRoles { get; }
    public HttpContext? HttpContextSession { get; }
}
