using SFMB.DAL.Entities;

namespace SFMB.DAL.Repositories.Interfaces
{
    public interface IOperationTypeRepository
    {
        Task<IEnumerable<OperationType>> GetAllAsync();
        Task<IEnumerable<OperationType>> GetAllByUserAsync(string userId);
        Task<OperationType?> GetByIdAsync(int id);
        Task<OperationType?> GetByIdAndUserAsync(int id, string userId);
        Task<OperationType> AddAsync(OperationType entity);
        Task<OperationType> UpdateAsync(OperationType entity);
        Task<bool> DeleteAsync(int id);
    }
}
