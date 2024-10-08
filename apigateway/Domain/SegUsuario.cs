using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SegUsuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public int? GdiaPersonalId { get; set; }
        //public virtual GdiaPersonal GdiaPersonal { get; set; }
        public string? ApellidoNombre { get; set; }
        public int? ProveedorId { get; set; }
        public int ClienteId { get; set; }
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
        public string Contrasena { get; set; }
        public string? ExternalPassword { get; set; }
        public int? ContrasenaTipo { get; set; }
        public string? Mail { get; set; }
        public string? MailSecundario { get; set; }
        public string? ContrasenaPregunta { get; set; }
        public string? ContrasenaRespuesta { get; set; }
        public DateTime? UltimoDiaLogueado { get; set; }
        public DateTime? UltimoDiaContraseñaCambiada { get; set; }
        public int? CantidadIntentoFallidoCambioContraseña { get; set; }
        public DateTime? IntentoFallidoCambioContraseñaFecha { get; set; }
        public int? CantidadIntentoFallidoCambioRespuesta { get; set; }
        public DateTime? IntentoFallidoCambioRespuestaFecha { get; set; }
        public string? Comentario { get; set; }
        public DateTime? AuditoriaInsertDate { get; set; }
        public string? AuditoriaInsterUser { get; set; }
        public DateTime? AuditoriaUpdateDate { get; set; }
        public string? AuditoriaUpdateUser { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        public virtual ICollection<SegGrupo>? SegGrupo { get; set; }
        public bool PermiteIpPublica { get; set; }
        public GdiaPersonal? GdiaPersonal { get; set; } 
    }
}
