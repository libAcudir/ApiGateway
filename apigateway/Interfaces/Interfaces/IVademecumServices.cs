using Common.DTO;

namespace Common.Interfaces
{
    public interface IVademecumServices
    {
        Task<ResponseVademecumBaseMonodrogaDto> GetVademecumBaseMonodroga(string? droga);
        Task<ResponseVademecumBaseDto> GetVademecumbase(VademecumBaseRequestDto request);
    }
}

