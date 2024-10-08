using Common.Interfaces;
using Domain;
using NLog;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SegUsuarioRepository : ISegUsuarioRepository
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private AuthContext _dbContext;
        public SegUsuarioRepository(AuthContext dbContext)
        {           
            this._dbContext = dbContext;
        }

        public SegUsuario GetUserforName(string username)
        {
            try
            {
                var usuario = this._dbContext.SegUsuario.FirstOrDefault(u => u.Descripcion.ToUpper() == username.ToUpper());
                return usuario;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error durante la autenticación del usuario.");
                throw;
            }
        }

        public SegUsuario GetUserforDNI(string dni)
        {
            try
            {
                var segUsuario = _dbContext.SegUsuario
                        .Include(s => s.GdiaPersonal)
                        .ThenInclude(g => g.DireccionProvincia)
                        .Include(s => s.GdiaPersonal.GdiaPersonalEspecialidades)
                        .Where(s => s.GdiaPersonal.Dni == dni)
                        .FirstOrDefault();
                return segUsuario;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "El usuario que desea ingresar, no se encuentra registrado");
                throw;
            }
        }
    }
}