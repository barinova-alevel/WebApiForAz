using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SFMB.DAL.Entities;
using SFMB.DAL.Repositories.Interfaces;

namespace SFMB.DAL.Repositories
{
    public class OperationTypeRepository : IOperationTypeRepository
    {
        private readonly SfmbDbContext _context;

        public OperationTypeRepository(SfmbDbContext context)
        {
            _context = context;
        }

        public async Task<OperationType> AddAsync(OperationType entity)
        {
            if (entity == null)
            {
                Log.Error("OperationType entity cannot be null.");
                return null;
            }

            Log.Information($"Adding OperationType: {entity.Name}, IsIncome: {entity.IsIncome}");
            _context.OperationTypes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var operationType = _context.OperationTypes.Find(id);
            if (operationType == null)
            {
                Log.Warning($"OperationType with ID {id} not found for deletion.");
                return false;
            }
            Log.Information($"Deleting OperationType with ID: {id}");
            _context.OperationTypes.Remove(operationType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OperationType>> GetAllAsync()
        {
            var operationTypes = await _context.OperationTypes
                .Include(ot => ot.Operations)
                .ToListAsync();
            Log.Information("Retrieving all OperationTypes with their associated Operations.");
            return operationTypes;
        }

        public async Task<IEnumerable<OperationType>> GetAllByUserAsync(string userId)
        {
            var operationTypes = await _context.OperationTypes
                .Include(ot => ot.Operations)
                .Where(ot => ot.UserId == userId)
                .ToListAsync();
            Log.Information($"Retrieving OperationTypes for user {userId}.");
            return operationTypes;
        }

        public async Task<OperationType?> GetByIdAsync(int id)
        {
            var operationType = await _context.OperationTypes
                .Include(ot => ot.Operations)
                .FirstOrDefaultAsync(ot => ot.OperationTypeId == id);

            if (operationType == null)
            {
                Log.Warning($"OperationType with ID {id} not found.");
            }
            else
            {
                Log.Information($"Retrieved OperationType: {operationType.Name}, IsIncome: {operationType.IsIncome}");
            }
            return operationType;
        }

        public async Task<OperationType?> GetByIdAndUserAsync(int id, string userId)
        {
            var operationType = await _context.OperationTypes
                .Include(ot => ot.Operations)
                .FirstOrDefaultAsync(ot => ot.OperationTypeId == id && ot.UserId == userId);

            if (operationType == null)
            {
                Log.Warning($"OperationType with ID {id} not found for user {userId}.");
            }
            else
            {
                Log.Information($"Retrieved OperationType: {operationType.Name}, IsIncome: {operationType.IsIncome} for user {userId}");
            }
            return operationType;
        }

        public async Task<OperationType> UpdateAsync(OperationType operationType)
        {
            if (operationType == null)
            {
                Log.Error("OperationType entity cannot be null.");
                return null;
            }

            Log.Information($"Updating OperationType: {operationType.Name}, IsIncome: {operationType.IsIncome}");
            _context.OperationTypes.Update(operationType);
            await _context.SaveChangesAsync();
            return operationType;
        }
    }

}
