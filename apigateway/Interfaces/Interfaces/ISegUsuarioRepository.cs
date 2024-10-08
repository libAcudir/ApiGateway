using Domain;

namespace Common.Interfaces
{
    public interface ISegUsuarioRepository
    {
        SegUsuario GetUserforName(string username);
        SegUsuario GetUserforDNI(string dni);
    }
}
