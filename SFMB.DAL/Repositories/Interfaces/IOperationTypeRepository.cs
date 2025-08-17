using SFMB.DAL.Entities;

namespace SFMB.DAL.Repositories.Interfaces
{
    public interface IOperationTypeRepository
    {
        Task<IEnumerable<OperationType>> GetAllAsync();
        Task<OperationType?> GetByIdAsync(int id);
        Task<OperationType> AddAsync(OperationType entity);
        Task<OperationType> UpdateAsync(OperationType entity);
        Task<bool> DeleteAsync(int id);
    }
}
