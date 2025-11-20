using Microsoft.EntityFrameworkCore;
using SFMB.DAL.Entities;
using SFMB.DAL.Repositories.Interfaces;

namespace SFMB.DAL.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly SfmbDbContext _context;

        public OperationRepository(SfmbDbContext context)
        {
            _context = context;
        }

        public async Task<Operation> AddAsync(Operation operation)
        {
            await _context.Operations.AddAsync(operation);
            await _context.SaveChangesAsync();
            return operation;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var operation = await _context.Operations.FindAsync(id);
            if (operation == null)
            {
                return false;
            }

            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Operation>> GetAllAsync()
        {
            return await _context.Operations
                .Include(o => o.OperationType)
                .OrderByDescending(o => o.Date)
                .ToListAsync();
        }

        public async Task<Operation?> GetByIdAsync(int id)
        {
            return await _context.Operations
                 .Include(o => o.OperationType)
                 .FirstOrDefaultAsync(o => o.OperationId == id);
        }

        public async Task<Operation> UpdateAsync(Operation operation)
        {
            _context.Operations.Update(operation);
            await _context.SaveChangesAsync();
            return operation;
        }
    }

}
