using Common.DTO;
using Common.Interfaces;
using Domain;
using Microsoft.Extensions.Configuration;
using NLog;
using Services;

namespace Services
{
    public class UserService : IUserService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;
        private readonly ISegUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;
        public UserService(IConfiguration configuration, ISegUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }
        public ResponseLoginDTO Login(string dni, string password)
        {
            ResponseLoginDTO responseLogin = new ResponseLoginDTO();
            try
            {
                var usuario = _usuarioRepository.GetUserforDNI(dni);
                if (usuario == null || !usuario.Activo)
                {
                    logger.Info($"Usuario autenticado inexistente o inactivo: {dni}");
                    responseLogin.Error = "Usuario autenticado inexistente o inactivo";
                    responseLogin.Success = false;
                    return responseLogin;
                }

                if (!ValidatePassword(password, usuario))
                {
                    logger.Info($"¡La contraseña ingresada es incorrecta!");
                    responseLogin.Error = "¡La contraseña ingresada es incorrecta!";
                    responseLogin.Success = false;
                    return responseLogin;
                }

                responseLogin.Success = true;
                responseLogin.User = usuario;
                responseLogin.Token = _tokenService.GenerateToken(usuario.Descripcion, usuario.UsuarioId);
                return responseLogin;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error durante la validacion de credenciales.");
                responseLogin.Success = false;
                responseLogin.Error = "Error durante la validación de credenciales.";
                return responseLogin;
            }
        }

        public int Autenticar(string username, string password)
        {
            try
            {
                var usuario = _usuarioRepository.GetUserforName(username);
                if (usuario != null && !usuario.Activo)
                {
                    logger.Info($"Usuario autenticado inexistente o inactivo: {username}");
                    return 0;
                }
                if (ValidatePassword(password, usuario))
                {
                    logger.Info($"Usuario autenticado correctamente. ID: {username}");
                    return usuario.UsuarioId;
                }
                return 0;

            }
            catch (System.Exception ex)
            {
                logger.Error(ex, "Error durante la validacion de credenciales.");
                return 0;
                throw;
            }

        }
        public bool ValidatePassword(string password, SegUsuario usuario)
        {
            try
            {
                var passworDecryp = SecurityUtility.Instance.Decrypt(usuario.Contrasena);
                if (password != passworDecryp)
                {
                    logger.Error("La password ingresada es incorrecta");
                    return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                logger.Error(ex, "La password ingresada es incorrecta");
                return false;
                throw;
            }
        }
    }
}