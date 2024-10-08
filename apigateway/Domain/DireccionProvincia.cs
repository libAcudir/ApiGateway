using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class DireccionProvincia
    {
        public int DireccionProvinciaId { get; set; }
        public string Descripcion { get; set; }
        public string Jurisdiccion { get; set; }
        public bool Borrado { get; set; }
        public int? CapitalDireccionLocalidadId { get; set; }
        [MaxLength(1)]
        public string MatriculaMedicaProvincia { get; set; }
        public GdiaPersonal? GdiaPersonal { get; set; }
    }
}
