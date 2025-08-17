using SFMB.BL.Dtos;

namespace SFMB.BL.Services.Interfaces
{
    public interface IOperationTypeService
    {
        Task<OperationTypeDto> CreateAsync(OperationTypeDto entity);
        Task<OperationTypeDto> ReadAsync(int id);
        Task<IEnumerable<OperationTypeDto>> GetAllAsync();
        Task<OperationTypeDto> UpdateAsync(int id, OperationTypeDto entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<OperationTypeDto>> GetByIsIncomeAsync(bool isIncome);
    }
}
