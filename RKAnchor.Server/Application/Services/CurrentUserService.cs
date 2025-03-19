using RKAnchor.Server.Domain.Interfaces.IServices;
using System.Security.Claims;

namespace RKAnchor.Server.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    public string? UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name;
    public string? UserEmail => _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    public string? UserRole => _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
    public IEnumerable<string>? UserRoles => _httpContextAccessor.HttpContext?.User?.Claims
        .Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value);
    public bool? IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated;
    public HttpContext? HttpContextSession => _httpContextAccessor?.HttpContext;
}
