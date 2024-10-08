
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;


namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("token")]
        public IActionResult Token(string username, string password)
        {
            try
            {
                int userId = _userService.Autenticar(username, password);
                if (userId == 0) { Logger.Info($"Las credenciales ingresadas son incorrectas '{username}'."); return StatusCode(401, "Las credenciales ingresadas son incorrectas"); }
                var token = _tokenService.GenerateToken(username, userId);
                Logger.Info($"Token generado para el usuario '{username}'.");
                return Ok(new
                {
                    token
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al generar el token.");

                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}