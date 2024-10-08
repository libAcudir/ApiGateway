using Common.DTO;

namespace Common.Interfaces
{
    public interface IUserService 
    {
        public int Autenticar(string userName, string password);
        public ResponseLoginDTO Login(string dni, string password);
    }
}
