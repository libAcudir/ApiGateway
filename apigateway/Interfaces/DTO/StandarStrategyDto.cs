namespace Common.DTO
{
    public class StandardResponse
    {
        public IEnumerable<DatosAfiliatoriosDto>? DataAfiliadosDto { get; set; } = new List<DatosAfiliatoriosDto>();
        public string? Source { get; set; } // Opcional, para saber de qué cliente proviene
        public string? ErrorMessage { get; set; }
    }

    public class RequestParameters
    {
        public int? ContratoId { get; set; }
        public string? Nombre { get; set; }
        public string? Nro { get; set; }
        public string? Dni { get; set; }
        public bool Activo { get; set; }
        public bool BuscarOnline { get; set; }
    }

}
