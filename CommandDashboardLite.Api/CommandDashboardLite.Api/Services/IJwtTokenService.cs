using CommandDashboardLite.Api.Models;

namespace CommandDashboardLite.Api.Services;

public interface IJwtTokenService
{
    string CreateToken(User user);
}
