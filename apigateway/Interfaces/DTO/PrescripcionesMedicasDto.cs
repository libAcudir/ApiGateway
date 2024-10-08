using Domain;

namespace Common.DTO
{
    public class ResponsePrescripcionDTO
    {
        public bool Success { get; set; }
        public IEnumerable<PrescripcionesMedicasDTO>? ListDatos { get; set; } = new List<PrescripcionesMedicasDTO>();
        public string? Error { get; set; }
    }
    public class ResponseDetalleDTO
    {
        public bool Success { get; set; }
        public IEnumerable<PrescripcionDetalleDto>? ListDatos { get; set; } = new List<PrescripcionDetalleDto>();
        public string? Error { get; set; }
    }
    public class PrescripcionesMedicasDTO
    {
        public int PrescripcionId { get; set; }
        public int? PedidoId { get; set; }
        public int? ClienteId { get; set; }
        public int PrescripcionValidadorId { get; set; }
        public string NombreApellido { get; set; }
        public string ObraSocial { get; set; }
        public string Plan { get; set; }
        public string NroAfiliado { get; set; }
        public string Diagnostico { get; set; }
        public int PrescripcionEstadoId { get; set; }
        public bool Activo { get; set; }
        public string NroRecetaFinanciadora { get; set; }
        public string Credencial { get; set; }
        public string DocumentoNro { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public int MedicoId { get; set; }
        public IEnumerable<PrescripcionDetalleDto> PrescripcionDetalle { get; set; }
        public IEnumerable<IndicacionDto> Indicacion { get; set; }
    }

    public class PrescripcionDetalleDto
    {
        public int PrescripcionId { get; set; }
        public int PrescripcionDetalleId { get; set; }
        public int Cantidad { get; set; }
        public bool Activo { get; set; }
        public int VademecumBaseId { get; set; }
        public bool Generico { get; set; }
        public string Posologia { get; set; }
        public int NumeroDeReceta { get; set; }
    }
    public partial class IndicacionDto
    {
        public int IndicacionId { get; set; }
        public int PrescripcionId { get; set; }
        public int TipoIndicacionId { get; set; }
        public string Descripcion { get; set; }
    }

}
