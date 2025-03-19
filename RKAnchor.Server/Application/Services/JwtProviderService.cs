using Microsoft.IdentityModel.Tokens;
using RKAnchor.Server.Application.Models.Configuration;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RKAnchor.Server.Application.Services;

public class JwtProviderService : IJwtProviderService
{
    private readonly JwtOptions _jwtOptions;

    public JwtProviderService(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.RoleName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("Nationality", user.Nationality),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.JwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_jwtOptions.JwtExpireDays);

        var token = new JwtSecurityToken(_jwtOptions.JwtIssuer, _jwtOptions.JwtIssuer, claims, expires: expires, signingCredentials: credentials);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}
