using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Domain.Interfaces.IServices;

public interface IJwtProviderService
{
    string GenerateJwtToken(User user);
}
