using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class ResponseDto
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
    public class ResponseVademecumBaseMonodrogaDto
    {
        public bool Success { get; set; }
        public IEnumerable<VademecumBaseMonodrogaDto>? ListDatos { get; set; } = new List<VademecumBaseMonodrogaDto>();
        public string? Error { get; set; }
    }

    public class ResponseVademecumBaseDto :ResponseDto
    {
        public IEnumerable<VademecumBaseDto>? Medicamentos { get; set; }=new List<VademecumBaseDto>();
    }
    public class VademecumBaseRequestDto
    {
        public int? VademecumBaseId { get; set; }
        public int? vademecumBaseMonodrogaId { get; set; } = null;
        public string nombreProducto { get; set; } 
        public string presentacion { get; set; } 
        public string descripcion { get; set; }
        //public bool activo { get; set; } = true;
    }
    public class VademecumBaseMonodrogaDto
    {
        public int VademecumBaseMonodrogaId { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }

    public class VademecumBaseDto
    {
        public int VademecumBaseId { get; set; }
        public int Registro { get; set; }
        public DateTime Vigencia { get; set; }
        public float? Precio { get; set; }
        public string Estado { get; set; }
        public string NombreProducto { get; set; }
        public string Presentacion { get; set; }
        public string Importado { get; set; }
        public string Heladera { get; set; }
        public int? Troquel { get; set; }
        public string CodigosBarra { get; set; }
        public string Atc { get; set; }
        public string Iva { get; set; }
        public int? Laboratorio { get; set; }
        public int? TipoVenta { get; set; }
        public int? Salud { get; set; }
        public int? Tamanio { get; set; }
        public int? FormaFarmaceutica { get; set; }
        public int? Via { get; set; }
        public int? VademecumBaseMonodrogaId { get; set; }
        public VademecumBaseMonodrogaDto VademecumBaseMonodroga { get; set; }
        public int? AccionFarmaceutica { get; set; }
        public int? VademecumBaseUnidadPotenciaId { get; set; }
        public VademecumBaseUnidadPotenciaDto VademecumBaseUnidadPotencia { get; set; }
        public string Potencia { get; set; }
        public int? VademecumBaseTipoUnidadId { get; set; }
        public VademecumBaseTipoUnidadDto VademecumBaseTipoUnidad { get; set; }
        public int? Unidades { get; set; }
        public string Gtins { get; set; }
        public string Gravamen { get; set; }
        public string Celiacos { get; set; }
        public string CodigoSnomed { get; set; }
        public int? Prospecto { get; set; }
        public string Cobertura { get; set; }
        public string DrogaDetalle { get; set; }
        public int? Ultimolog { get; set; }
        public bool UsoAcudir { get; set; } = true;
    }

    public class VademecumBaseUnidadPotenciaDto
    {
        public int VademecumBaseUnidadPotenciaId { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
    public class VademecumBaseTipoUnidadDto
    {
        public int VademecumBaseTipoUnidadId { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }

}
