using Common.DTO;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ApiGateway.Controllers
{
    public class LoginController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        public LoginController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequestDTO loginRequest)
        {
            // Verifico que el DNI y la contraseña no estén null
            if (string.IsNullOrWhiteSpace(loginRequest.Dni) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest(new { success = false, error = "DNI y Password son requeridos" });
            }

            try
            {
                var loginResponse = _userService.Login(loginRequest.Dni, loginRequest.Password);
                if (!loginResponse.Success)
                {
                    return Unauthorized(new { success = false, error = loginResponse.Error });
                }
                HttpContext.Session.SetString("UserDni", loginRequest.Dni);
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                // Registra el error y retorna un código de estado 500
                Logger.Error(ex, "Error durante el inicio de sesión");
                return StatusCode(500, new { success = false, error = "Error durante el login" });
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                // Opcional: Elimino la cookie de sesión si es necesario
                if (HttpContext.Request.Cookies.ContainsKey("ASP.NET_SessionId"))
                {
                    HttpContext.Response.Cookies.Delete("ASP.NET_SessionId");
                }
                return Ok(new { success = true, message = "¡Su sesión se cerró correctamente!" });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error durante el cierre de sesión");
                return StatusCode(500, new { success = false, error = "Error al cerrar la sesión, intente nuevamente más tarde" });
            }
        }

    }
}

