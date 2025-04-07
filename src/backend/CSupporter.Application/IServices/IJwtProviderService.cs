using CSupporter.Domain.Entities;

namespace CSupporter.Application.IServices;

public interface IJwtProviderService
{
    string GenerateJwtToken(User user);
}
