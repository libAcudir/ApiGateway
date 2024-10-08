using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class TokenService : ITokenService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username, int userId)
        {
            try
            {
                var secretKey = _configuration["SecretKey"];
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("u_id", userId.ToString()),
                    // Se pueden agregar más claims
                };

                var token = new JwtSecurityToken(
                    issuer: "your_issuer",
                    audience: "your_audience",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20), // Tiempo de expiración del token
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error al generar el token JWT.");
                throw; // Re-lanzar la excepción para manejo posterior
            }
        }

        public bool ValidateToken(string token, out ClaimsPrincipal principal)
        {
            principal = null;
            try
            {
                var secretKey = _configuration["SecretKey"];
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "your_issuer",
                    ValidAudience = "your_audience",
                    ClockSkew = TimeSpan.Zero // Ajustar si se necesita un margen para la expiración
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;

                // Validar el token
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                // Validar si el token es del tipo JwtSecurityToken
                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    // Validar si el token está expirado
                    if (jwtToken.ValidTo < DateTime.UtcNow)
                    {
                        logger.Warn("Token expirado.");
                        return false;
                    }
                }
                return true;
            }
            catch (SecurityTokenExpiredException ex)
            {
                logger.Warn(ex, "Token expirado.");
                return false;
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                logger.Warn(ex, "Firma del token inválida.");
                return false;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error al validar el token JWT.");
                return false;
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                var secretKey = _configuration["SecretKey"];
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "your_issuer",
                    ValidAudience = "your_audience",
                    ClockSkew = TimeSpan.Zero, // Ajustar si se necesita un margen para la expiración
                    RequireExpirationTime = true
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;

                // Validar el token incluso si está expirado
                return tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error al obtener principal del token expirado.");
                throw;
            }
        }
    }
}
