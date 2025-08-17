using SFMB.DAL.Entities;

namespace SFMB.DAL.Repositories.Interfaces
{
    public interface IOperationRepository
    {
        Task<Operation> AddAsync(Operation operation);
        Task<Operation?> GetByIdAsync(int id);
        Task<IEnumerable<Operation>> GetAllAsync();
        Task<Operation> UpdateAsync(Operation operation);
        Task<bool> DeleteAsync(int id);
    }
}
