namespace Common.DTO
{
    public class ResponsePrescripcionDto
    {
        public bool Success { get; set; }
       public string? Error { get; set; }
    }
    public class PrescripcionDto
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
    public class RequestPrescripcionPdfDto
    {
        public Guid DocumentTransactionId { get; set; }
        public int? RecetaElectronicaCodigo { get; set; }
        public int ClienteId { get; set; }
    }
}
