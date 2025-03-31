using CSupporter.API.Domain.Entities;

namespace CSupporter.API.Domain.Interfaces.IServices;

public interface IJwtProviderService
{
    string GenerateJwtToken(User user);
}
