namespace CSupporter.API.Domain.Interfaces.IServices;

public interface ICurrentUserService
{
    public string? UserId { get; }
    public string? UserEmail { get; }
    public string? UserName { get; }
    public string? UserRole { get; }
    public IEnumerable<string>? UserRoles { get; }
    public HttpContext? HttpContextSession { get; }
}
