using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace SecureMissionOps.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration config) : ControllerBase
{
    [HttpPost("token")]
    public IActionResult Token([FromBody] LoginDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username)) return Unauthorized();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, dto.Username),
            new Claim(ClaimTypes.Role, dto.Role ?? "Operator")
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(6),
            signingCredentials: creds);

        return Ok(new { access_token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    public class LoginDto
    {
        public string Username { get; set; } = default!;
        public string? Role { get; set; }
        public string? Password { get; set; } // unused in demo
    }
}
