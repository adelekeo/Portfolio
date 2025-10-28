namespace CommandDashboardLite.Api.Services;

using CommandDashboardLite.Api.Auth;
using CommandDashboardLite.Api.Models;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


public class JwtTokenService : IJwtTokenService
{
    private readonly AuthSettings _settings;

    public JwtTokenService(IOptions<AuthSettings> options) => _settings = options.Value;

    public string CreateToken(User user)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
        foreach (var r in user.Roles)
            claims.Add(new Claim(ClaimTypes.Role, r.Name));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
