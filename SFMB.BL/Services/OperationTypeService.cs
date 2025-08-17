using Serilog;
using SFMB.BL.Dtos;
using SFMB.BL.Services.Interfaces;
using SFMB.DAL.Repositories.Interfaces;

namespace SFMB.BL.Services
{
    public class OperationTypeService : IOperationTypeService
    {
        private readonly IOperationTypeRepository _operationTypeRepository;

        public OperationTypeService(IOperationTypeRepository operationTypeRepository)
        {
            _operationTypeRepository = operationTypeRepository;
        }

        public async Task<OperationTypeDto> CreateAsync(OperationTypeDto operationType)
        {
            if (operationType == null)
            {
                Log.Error("OperationTypeDto is null.");
                if (operationType == null)
                {
                    Log.Error("OperationTypeDto is null.");
                    return null;
                }
            }

            var newOperationType = new SFMB.DAL.Entities.OperationType
            {
                Name = operationType.Name,
                Description = operationType.Description,
                IsIncome = operationType.IsIncome
            };
            var createdOperationType = await _operationTypeRepository.AddAsync(newOperationType);
            Log.Information($"OperationType with id {createdOperationType.OperationTypeId} created successfully.");
            return new OperationTypeDto
            {
                OperationTypeId = createdOperationType.OperationTypeId,
                Name = createdOperationType.Name,
                Description = createdOperationType.Description,
                IsIncome = createdOperationType.IsIncome
            };
        }

        public async Task<OperationTypeDto> ReadAsync(int id)
        {
            var operationType = await _operationTypeRepository.GetByIdAsync(id);

            if (operationType == null)
            {
                Log.Information($"OperationType with id {id} not found.");
                return null;
            }

            Log.Information($"OperationType with id {id} found.");
            return new OperationTypeDto
            {
                OperationTypeId = operationType.OperationTypeId,
                Name = operationType.Name,
                Description = operationType.Description,
                IsIncome = operationType.IsIncome
            };
        }

        public async Task<IEnumerable<OperationTypeDto>> GetAllAsync()
        {
            var operationTypes = await _operationTypeRepository.GetAllAsync();
            Log.Information($"Retrieved {operationTypes.Count()} operation types from the repository.");

            if (operationTypes == null || !operationTypes.Any())
            {
                Log.Information("No operation types found.");
                return Enumerable.Empty<OperationTypeDto>();
            }

            return operationTypes.Select(ot => new OperationTypeDto
            {
                OperationTypeId = ot.OperationTypeId,
                Name = ot.Name,
                Description = ot.Description,
                IsIncome = ot.IsIncome,
                Operations = ot.Operations?.Select(op => new OperationDto
                {
                    OperationId = op.OperationId,
                    Date = op.Date,
                    Amount = op.Amount,
                    Note = op.Note,
                    OperationTypeId = op.OperationTypeId
                }).ToList() ?? new List<OperationDto>()
            });
        }

        public async Task<OperationTypeDto> UpdateAsync(int id, OperationTypeDto entity)
        {
            var operationType = await _operationTypeRepository.GetByIdAsync(id);

            if (operationType == null)
            {
                Log.Information($"OperationType with id {id} not found for update.");
                return null;
            }

            operationType.Name = entity.Name;
            operationType.Description = entity.Description;
            operationType.IsIncome = entity.IsIncome;

            var updatedOperationType = await _operationTypeRepository.UpdateAsync(operationType);
            Log.Information($"OperationType with id {id} updated successfully.");
            return new OperationTypeDto
            {
                OperationTypeId = updatedOperationType.OperationTypeId,
                Name = updatedOperationType.Name,
                Description = updatedOperationType.Description,
                IsIncome = updatedOperationType.IsIncome
            };
        }

        public async Task DeleteAsync(int id)
        {
            Log.Information($"Attempting to delete OperationType with id {id}.");
            await _operationTypeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OperationTypeDto>> GetByIsIncomeAsync(bool isIncome)
        {
            var allOperationTypes = await _operationTypeRepository.GetAllAsync();
            var isIncomeTypes = allOperationTypes.Where(ot => ot.IsIncome == isIncome);

            if (!isIncomeTypes.Any())
            {
                Log.Information($"No operation types found with IsIncome = {isIncome}.");
                return Enumerable.Empty<OperationTypeDto>();
            }

            Log.Information($"Found {isIncomeTypes.Count()} operation types with IsIncome = {isIncome}.");
            return isIncomeTypes.Select(ot => new OperationTypeDto
            {
                OperationTypeId = ot.OperationTypeId,
                Name = ot.Name,
                Description = ot.Description,
                IsIncome = ot.IsIncome
            });
        }
    }

}
