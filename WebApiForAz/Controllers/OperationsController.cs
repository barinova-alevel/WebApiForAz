using Microsoft.AspNetCore.Mvc;
using SFMB.BL.Dtos;
using SFMB.BL.Services.Interfaces;
using SFMB.DAL.Entities;

namespace WebApiForAz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationsController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        // GET: api/Operations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operation>>> GetAllOperations()
        {
            var operations = await _operationService.GetAllAsync();

            if (operations == null || !operations.Any())
            {
                return NotFound();
            }

            return Ok(operations.ToList());
        }

        // GET: api/Operations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operation>> GetOperation(int id)
        {
            var operation = await _operationService.GetByIdAsync(id);

            if (operation == null)
            {
                return BadRequest($"Operation with id {id} not found");
            }

            return Ok(operation);
        }

        // PUT: api/Operations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperation(int id, SFMB.DAL.Entities.Operation operation)
        {
            if (id != operation.OperationId)
            {
                return BadRequest("Operation Id mismatch.");
            }

            var operationDto = new OperationDto
            {
                OperationId = operation.OperationId,
                Date = operation.Date,
                Amount = operation.Amount,
                Note = operation.Note,
                OperationTypeId = operation.OperationTypeId
            };

            var updatedDto = await _operationService.UpdateAsync(id, operationDto);

            if (updatedDto == null)
            {
                return NotFound($"Operation with id {id} not found.");
            }

            return Ok(updatedDto);
        }

        // POST: api/Operations
        [HttpPost]
        public async Task<ActionResult<Operation>> PostOperation(Operation operation)
        {
            var validationResult = ValidateOperation(operation);
            if (validationResult is not null)
                return validationResult;

            var operationDto = new OperationDto
            {
                Date = operation.Date,
                Amount = operation.Amount,
                Note = operation.Note,
                OperationTypeId = operation.OperationTypeId
            };
            var createdOperation = await _operationService.CreateAsync(operationDto);

            if (createdOperation == null)
            {
                return BadRequest("Failed to create Operation.");
            }

            var checkNewOperation = await _operationService.GetByIdAsync(createdOperation.OperationId);

            if (checkNewOperation == null)
            {
                return BadRequest($"Operation with id {createdOperation.OperationTypeId} not found after creation.");
            }

            var createdOperationEntity = new Operation
            {
                OperationId = createdOperation.OperationId,
                Date = createdOperation.Date,
                Amount = createdOperation.Amount,
                Note = createdOperation.Note,
                OperationTypeId = createdOperation.OperationTypeId
            };
            return CreatedAtAction(nameof(GetOperation), new { id = createdOperationEntity.OperationId }, createdOperationEntity);
        }

        // DELETE: api/Operations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            var operation = await _operationService.GetByIdAsync(id);

            if (operation == null)
            {
                return NotFound($"Operation with id {id} not found.");
            }

            var isDeleted = await _operationService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest($"Failed to delete Operation with id {id}.");
            }

            return NoContent();
        }

        private ActionResult? ValidateOperation(Operation? operation)
        {
            if (operation == null)
                return BadRequest("Operation data is required.");

            if (operation.Amount == default)
                return BadRequest("Amount is required.");

            if (operation.OperationTypeId == default)
                return BadRequest("OperationTypeId is required.");

            return null;
        }
    }
}
