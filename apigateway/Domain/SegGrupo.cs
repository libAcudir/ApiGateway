using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SegGrupo
    {
        [Key]
        public int GrupoId { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionAbreviada { get; set; } 
        public bool Activo { get; set; }
    }
}
