using System.Security.Claims;

namespace Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username, int userId);
        bool ValidateToken(string token, out ClaimsPrincipal principal);
    }
}
