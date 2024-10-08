namespace Common.DTO
{
    public class DatosAfiliatoriosDto
    {
        public string ContratoDescripcion { get; set; }
        public string ContratoPlanDescripcion { get; set; }
        public string NroAfiliado { get; set; }
        public string NombreAfiliado { get; set; }
        public string TelefonoAfiliado { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? TipoDocumentoId { get; set; }
        public string TipoDocumento { get; set; }
        public string DocumentoNro { get; set; }
        public int? ContratoPlanId { get; set; }
        public int? ContratoAfiliadoId { get; set; }
        public int ContratoId { get; set; }
        public string Domicilio { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string CalleAdyacente1 { get; set; }
        public string CalleAdyacente2 { get; set; }
        public string Localidad { get; set; }
        public int? ContratoPadronId { get; set; } = 0;
        public bool? ContratoActivo { get; set; } = true;
        public bool? Activo { get; set; } = true;
        public string Dni { get; set; }
        public string Nro { get; set; }
        public string Nombre { get; set; }
        public bool? PacienteRecomendado { get; set; } = false;
        public bool? PersonajePublico { get; set; } = false;
        public string Telefono { get; set; }
        public bool? ExentoCoseguro { get; set; } = false;
        public string FechaBaja { get; set; }
        public bool? PMI { get; set; } = false;
        public bool? Discapacidad { get; set; } = false;
        public string DomicilioCodigoPostal { get; set; }
        public string Parentesco { get; set; }
        public string Credencial { get; set; }
        public int? PedidoId { get; set; } = null;
        public DateTime? DiscapacidadDesde { get; set; }
        public DateTime? DiscapacidadHasta { get; set; }

        public int? Edad
        {
            get
            {
                if (FechaNacimiento.HasValue)
                {
                    int age = new DateTime((DateTime.Now - Convert.ToDateTime(FechaNacimiento)).Ticks).Year;
                    return age - 1;
                }
                return 0;
            }
        }
    }
}
