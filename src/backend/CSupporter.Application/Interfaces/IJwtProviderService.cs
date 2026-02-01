using CSupporter.Domain.Entities;

namespace CSupporter.Application.Interfaces;

public interface IJwtProviderService
{
    string GenerateJwtToken(User user);
}
