using Domain;

namespace Common.DTO
{
    public class ResponseLoginDTO
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public SegUsuario User { get; set; }
        public string Error { get; set; }
    }
    public class ResponseDatosAfiliadosDTO
    {
        public bool Success { get; set; }
        public IEnumerable<DatosAfiliatoriosDto>? ListDatos { get; set; } = new List<DatosAfiliatoriosDto>();
        public string? Error { get; set; }
    }
}
