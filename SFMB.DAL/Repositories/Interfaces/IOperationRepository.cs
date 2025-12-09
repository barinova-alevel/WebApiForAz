using SFMB.DAL.Entities;

namespace SFMB.DAL.Repositories.Interfaces
{
    public interface IOperationRepository
    {
        Task<Operation> AddAsync(Operation operation);
        Task<Operation?> GetByIdAsync(int id);
        Task<Operation?> GetByIdAndUserAsync(int id, string userId);
        Task<IEnumerable<Operation>> GetAllAsync();
        Task<IEnumerable<Operation>> GetAllByUserAsync(string userId);
        Task<Operation> UpdateAsync(Operation operation);
        Task<bool> DeleteAsync(int id);
    }
}
