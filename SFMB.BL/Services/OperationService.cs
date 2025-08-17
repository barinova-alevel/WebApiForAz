using Serilog;
using SFMB.BL.Dtos;
using SFMB.BL.Services.Interfaces;
using SFMB.DAL.Repositories.Interfaces;

namespace SFMB.BL.Services
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _operationRepository;
        public OperationService(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task<OperationDto> CreateAsync(OperationDto operation)
        {
            var operationEntity = new SFMB.DAL.Entities.Operation
            {
                Date = operation.Date,
                Amount = operation.Amount,
                Note = operation.Note,
                OperationTypeId = operation.OperationTypeId
            };

            var createdOperation = await _operationRepository.AddAsync(operationEntity);
            Log.Logger.Information($"Operation with id {createdOperation.OperationId} created successfully.");

            return new OperationDto
            {
                OperationId = createdOperation.OperationId,
                Date = createdOperation.Date,
                Amount = createdOperation.Amount,
                Note = createdOperation.Note,
                OperationTypeId = createdOperation.OperationTypeId
            };
        }

        public async Task<OperationDto?> GetByIdAsync(int id)
        {
            var operation = await _operationRepository.GetByIdAsync(id);

            if (operation == null)
            {
                Log.Logger.Information($"Operation with id {id} not found.");
                return null;
            }

            Log.Logger.Information($"Operation with id {id} found.");
            return new OperationDto
            {
                OperationId = operation.OperationId,
                Date = operation.Date,
                Amount = operation.Amount,
                Note = operation.Note,
                OperationTypeId = operation.OperationTypeId
            };
        }

        public async Task<IEnumerable<OperationDto>> GetAllAsync()
        {
            Log.Logger.Information("Fetching all operations from the repository.");

            var operations = await _operationRepository.GetAllAsync();
            return operations.Select(op => new OperationDto
            {
                OperationId = op.OperationId,
                Date = op.Date,
                Amount = op.Amount,
                Note = op.Note,
                OperationTypeId = op.OperationTypeId
            });
        }

        public async Task<OperationDto> UpdateAsync(int id, OperationDto operationDto)
        {
            var operation = await _operationRepository.GetByIdAsync(id);

            if (operation == null)
            {
                Log.Logger.Information($"Operation with id {id} not found for update.");
                return null;
            }

            operation.Date = operationDto.Date;
            operation.Amount = operationDto.Amount;
            operation.Note = operationDto.Note;
            operation.OperationTypeId = operationDto.OperationTypeId;

            var updatedOperation = await _operationRepository.UpdateAsync(operation);
            Log.Information($"Operation with id {id} updated successfully.");

            return new OperationDto
            {
                OperationId = updatedOperation.OperationId,
                Date = updatedOperation.Date,
                Amount = updatedOperation.Amount,
                Note = updatedOperation.Note,
                OperationTypeId = updatedOperation.OperationTypeId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var operation = await _operationRepository.GetByIdAsync(id);
            if (operation == null)
            {
                Log.Logger.Information($"Operation with id {id} not found for deletion.");
                return false;
            }
            var result = await _operationRepository.DeleteAsync(id);
            Log.Logger.Information($"Operation with id {id} deletion status: {result}.");
            return result;
        }
    }
}
