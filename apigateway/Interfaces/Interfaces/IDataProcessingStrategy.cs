using Common.DTO;

namespace Common.Interfaces
{
    public interface IDataProcessingStrategy
    {
        Task<StandardResponse> DataAsync(RequestParameters parameters);
    }
}
