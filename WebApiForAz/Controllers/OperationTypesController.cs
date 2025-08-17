using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SFMB.BL.Dtos;
using SFMB.DAL.Entities;
using OperationType = SFMB.DAL.Entities.OperationType;
using SFMB.BL.Services.Interfaces;

namespace WebApiForAz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypesController : ControllerBase
    {
        private readonly SFMB.BL.Services.Interfaces.IOperationTypeService _operationTypeService;
        private readonly SFMB.BL.Services.Interfaces.IOperationService _operationService;

        public OperationTypesController(IOperationTypeService operationTypeService, IOperationService operationService)
        {
            _operationTypeService = operationTypeService;
            _operationService = operationService;
        }

        // GET: api/OperationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationType>>> GetOperationTypes()
        {
            var operationTypeDtos = await _operationTypeService.GetAllAsync();

            if (operationTypeDtos == null || !operationTypeDtos.Any())
            {
                return NotFound();
            }

            var operationTypes = operationTypeDtos.Select(dto => new OperationType
            {
                OperationTypeId = dto.OperationTypeId,
                Name = dto.Name,
                Description = dto.Description,
                IsIncome = dto.IsIncome,
                Operations = dto.Operations?.Select(o => new Operation
                {
                    OperationId = o.OperationId,
                    Date = o.Date,
                    Amount = o.Amount,
                    Note = o.Note,
                    OperationTypeId = o.OperationTypeId
                }).ToList() ?? new List<Operation>()
            }).ToList();

            return Ok(operationTypes);
        }

        // GET: api/OperationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationType>> GetOperationType(int id)
        {
            var operationType = await _operationTypeService.ReadAsync(id);

            if (operationType == null)
            {
                return NotFound();
            }

            return Ok(operationType);
        }

        // PUT: api/OperationTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperationType(int id, OperationType operationType)
        {
            if (id != operationType.OperationTypeId)
            {
                return BadRequest("Operation Type Id mismatch.");
            }

            var operationTypeDto = new OperationTypeDto
            {
                OperationTypeId = operationType.OperationTypeId,
                Name = operationType.Name,
                Description = operationType.Description,
                IsIncome = operationType.IsIncome
            };

            var updatedOperationType = await _operationTypeService.UpdateAsync(id, operationTypeDto);

            if (updatedOperationType == null)
            {
                return NotFound($"Operation Type with id {id} not found.");
            }

            return Ok(updatedOperationType);
        }

        // POST: api/OperationTypes
        [HttpPost]
        public async Task<ActionResult<OperationType>> PostOperationType(OperationType operationType)
        {
            var operationTypeForCreationDto = new OperationTypeDto
            {
                Name = operationType.Name,
                Description = operationType.Description,
                IsIncome = operationType.IsIncome
            };
            var createdOperationTypeResult = await _operationTypeService.CreateAsync(operationTypeForCreationDto);

            if (createdOperationTypeResult == null)
            {
                return BadRequest("Failed to create Operation Type.");
            }

            if (operationType.Operations != null && operationType.Operations.Any())
            {
                foreach (var op in operationType.Operations)
                {
                    op.OperationTypeId = createdOperationTypeResult.OperationTypeId;

                    var operationDto = new OperationDto
                    {
                        Date = op.Date,
                        Amount = op.Amount,
                        Note = op.Note,
                        OperationTypeId = op.OperationTypeId
                    };

                    var createdOperation = await _operationService.CreateAsync(operationDto);

                    if (createdOperation == null)
                    {
                        return StatusCode(500, "Failed to create one or more associated operations.");
                    }
                }
            }

            //Read the created OperationType
            var fullOperationType = await _operationTypeService.ReadAsync(createdOperationTypeResult.OperationTypeId);

            if (fullOperationType == null)
            {
                return NotFound($"Operation Type with id {createdOperationTypeResult.OperationTypeId} not found after creation.");
            }

            //Return the created resource
            var responseOperationType = new OperationType
            {
                OperationTypeId = fullOperationType.OperationTypeId,
                Name = fullOperationType.Name,
                Description = fullOperationType.Description,
                IsIncome = fullOperationType.IsIncome,
                Operations = fullOperationType.Operations?.Select(o => new Operation
                {
                    OperationId = o.OperationId,
                    Date = o.Date,
                    Amount = o.Amount,
                    Note = o.Note,
                    OperationTypeId = o.OperationTypeId
                }).ToList()
            };
            return CreatedAtAction("GetOperationType", new { id = responseOperationType.OperationTypeId }, responseOperationType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationType(int id)
        {
            var operationType = await _operationTypeService.ReadAsync(id);

            if (operationType == null)
            {
                return NotFound($"Operation Type with id {id} not found.");
            }

            await _operationTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
