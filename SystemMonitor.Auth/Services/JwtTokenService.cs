using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SystemMonitor.Models.Entities;

namespace SystemMonitor.Auth.Services;

public class JwtTokenService(IConfiguration configuration)
{
    private readonly string? _key = configuration["JwtSettings:SecretKey"];
    private readonly string? _issuer = configuration["JwtSettings:Issuer"];
    private readonly string? _audience = configuration["JwtSettings:Audience"];
    private readonly int _tokenExpiration = int.Parse(configuration["JwtSettings:TokenLifetime"] ?? "30");
    
    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };
        
        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddMinutes(_tokenExpiration),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}