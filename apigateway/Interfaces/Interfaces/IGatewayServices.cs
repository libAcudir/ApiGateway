using Common.DTO;

namespace Common.Interfaces
{
    public interface IGatewayServices //: IDataProcessingStrategy
    {
     Task<ResponseDatosAfiliadosDTO> GetDatosAfiliados(int? contratoId = null, string? nombre = null, string? nro = null, string? dni = null, bool activo = true, bool buscarOnline = true);
    }
}

