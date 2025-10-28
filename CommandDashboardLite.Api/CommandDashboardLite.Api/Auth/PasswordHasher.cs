using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CommandDashboardLite.Api.Auth;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100_000, 32);
        return Convert.ToBase64String(salt.Concat(hash).ToArray());
    }

    public static bool Verify(string password, string stored)
    {
        var bytes = Convert.FromBase64String(stored);
        var salt = bytes[..16];
        var hash = bytes[16..];
        var test = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100_000, 32);
        return CryptographicOperations.FixedTimeEquals(hash, test);
    }
}
