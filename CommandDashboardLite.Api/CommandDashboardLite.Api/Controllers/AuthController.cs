using CommandDashboardLite.Api.Auth;
using CommandDashboardLite.Api.Data;
using CommandDashboardLite.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommandDashboardLite.Api.Controllers;

public record LoginRequest(string Username, string Password);

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IJwtTokenService _jwt;

    public AuthController(AppDbContext db, IJwtTokenService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginRequest req)
    {
        var user = await _db.Users.Include(u => u.Roles)
            .SingleOrDefaultAsync(u => u.Username == req.Username);

        if (user is null) return Unauthorized();

        if (!PasswordHasher.Verify(req.Password, user.PasswordHash))
            return Unauthorized();

        var token = _jwt.CreateToken(user);
        return Ok(token);
    }
}
