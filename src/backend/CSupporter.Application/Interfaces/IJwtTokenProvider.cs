using CSupporter.Domain.Entities;

namespace CSupporter.Application.Interfaces;

public interface IJwtTokenProvider
{
    string GenerateJwtToken(User user);
}
