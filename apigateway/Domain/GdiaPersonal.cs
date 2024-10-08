using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class GdiaPersonal
    {
        [Key]
        public int GdiaPersonalId { get; set; }
        public int? GdiaPersonalSuspendidoId { get; set; }
        public int? SoftlandPersonalId { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public bool Suspendido { get; set; } = false;

        [Timestamp]
        public byte[] Timestamp { get; set; }
        public System.DateTime? AuditoriaInsertDate { get; set; }
        public string? AuditoriaInsertUser { get; set; }
        public DateTime? AuditoriaUpdateDate { get; set; }
        public string? AuditoriaUpdateUser { get; set; }
        public string? DatosContacto { get; set; }
        public int? BioStarnUserIdn { get; set; }
        public int? BioStarnUserId { get; set; }
        public string? Legajo { get; set; }
        public int? GdiaPersonalMatriculaTipoId { get; set; }
        public int? ProveedorId { get; set; }
        public int? GdiaPersonalEspecialidadId { get; set; }
        public int? CategoriaId { get; set; }
        public string? Telefono { get; set; }
        public string? Mail { get; set; }
        public bool EnviarSms { get; set; }
        public bool EnviarMail { get; set; }
        public string? Dni { get; set; }
        public bool? UsuarioTGO { get; set; }
        public DateTime? ProcesoRaetUpdateDate { get; set; }
        public bool? MedicoVisitas { get; set; }
        public bool? MovilidadPropia { get; set; }
        public int? CmnCelularId { get; set; }
        public int? ContratoAfiliadoId { get; set; }
        public string? Observacion { get; set; }
        public int? LimiteServiciosDia { get; set; }
        public bool? SoloHombres { get; set; }
        public bool? SoloMujeres { get; set; }
        public string? Sexo { get; set; }
        public string? Matricula { get; set; }
        public int? GdiaPersonalCategoriaRaetId { get; set; }
        public string? FirmaMedico { get; set; }
        public string? Cuit { get; set; }
        public int? DireccionProvinciaId { get; set; }
        public string? Domicilio { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public ICollection<GdiaPersonalEspecialidad>? GdiaPersonalEspecialidades { get; set; }
        public DireccionProvincia? DireccionProvincia { get; set; }
    }
}
