using Common.DTO;

namespace Common.Interfaces
{
    public interface IPrescripcionesServices
    {
        Task<ResponsePrescripcionDTO> GetPrescripciones(Guid documentTransactionId);
        Task<ResponseDetalleDTO> GetPrescripcionesDetalle(Guid documentTransactionId);
        Task<ResponsePrescripcionDto> UpdatePrescripcion(PrescripcionDto prescripcionDto);
        Task<ResponsePrescripcionDto> DeletePrescripcion(int prescripcionId);
        Task<ResponsePrescripcionDto> AddPrescripcion(PrescripcionDto prescripcionDto);
        Task<ResponseFileDto> GetPrescripcionPdf(RequestPrescripcionPdfDto prescripcionPdfDto);
    }
}