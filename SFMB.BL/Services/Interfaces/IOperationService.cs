using SFMB.BL.Dtos;

namespace SFMB.BL.Services.Interfaces
{
    public interface IOperationService
    {
        Task<OperationDto> CreateAsync(OperationDto operation);
        Task<OperationDto?> GetByIdAsync(int id);
        Task<IEnumerable<OperationDto>> GetAllAsync();
        Task<OperationDto> UpdateAsync(int id, OperationDto operation);
        Task<bool> DeleteAsync(int id);
    }
}
