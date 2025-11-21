using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using SFMB.BL.Dtos;
using SFMB.BL.Services;
using SFMB.DAL.Entities;
using SFMB.DAL.Repositories.Interfaces;
using Xunit;

namespace SFMB.Tests
{
    public class OperationServiceTests
    {
        private readonly Mock<IOperationRepository> _repoMock;
        private readonly OperationService _service;

        public OperationServiceTests()
        {
            _repoMock = new Mock<IOperationRepository>();
            _service = new OperationService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ReturnsMappedDto()
        {
            var dto = new OperationDto
            {
                Date = new DateOnly(2025, 1, 2),
                Amount = 123.45m,
                Note = "note",
                OperationTypeId = 1
            };

            var createdEntity = new Operation
            {
                OperationId = 10,
                Date = dto.Date,
                Amount = dto.Amount,
                Note = dto.Note,
                OperationTypeId = dto.OperationTypeId
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Operation>()))
                     .ReturnsAsync(createdEntity);

            var result = await _service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(createdEntity.OperationId, result.OperationId);
            Assert.Equal(createdEntity.Date, result.Date);
            Assert.Equal(createdEntity.Amount, result.Amount);
            Assert.Equal(createdEntity.Note, result.Note);
            Assert.Equal(createdEntity.OperationTypeId, result.OperationTypeId);

            _repoMock.Verify(r => r.AddAsync(It.IsAny<Operation>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenNotFound_ReturnsNull()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((Operation?)null);

            var result = await _service.GetByIdAsync(5);

            Assert.Null(result);
            _repoMock.Verify(r => r.GetByIdAsync(5), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenFound_ReturnsDto()
        {
            var entity = new Operation
            {
                OperationId = 7,
                Date = new DateOnly(2024, 12, 31),
                Amount = 50m,
                Note = "found",
                OperationTypeId = 2
            };

            _repoMock.Setup(r => r.GetByIdAsync(entity.OperationId))
                     .ReturnsAsync(entity);

            var result = await _service.GetByIdAsync(entity.OperationId);

            Assert.NotNull(result);
            Assert.Equal(entity.OperationId, result.OperationId);
            Assert.Equal(entity.Date, result.Date);
            Assert.Equal(entity.Amount, result.Amount);
            Assert.Equal(entity.Note, result.Note);
            Assert.Equal(entity.OperationTypeId, result.OperationTypeId);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllMapped()
        {
            var list = new List<Operation>
            {
                new Operation { OperationId = 1, Date = new DateOnly(2025,1,1), Amount = 10m, Note = "a", OperationTypeId = 1 },
                new Operation { OperationId = 2, Date = new DateOnly(2025,2,1), Amount = 20m, Note = "b", OperationTypeId = 2 }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

            var result = await _service.GetAllAsync();

            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Contains(resultList, r => r.OperationId == 1 && r.Note == "a");
            Assert.Contains(resultList, r => r.OperationId == 2 && r.Note == "b");
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ReturnsNull()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((Operation?)null);

            var updateDto = new OperationDto { Date = new DateOnly(2025, 1, 1), Amount = 1m, Note = "n", OperationTypeId = 1 };
            var result = await _service.UpdateAsync(100, updateDto);

            Assert.Null(result);
            _repoMock.Verify(r => r.GetByIdAsync(100), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenFound_UpdatesAndReturnsDto()
        {
            var existing = new Operation
            {
                OperationId = 20,
                Date = new DateOnly(2024, 1, 1),
                Amount = 5m,
                Note = "old",
                OperationTypeId = 1
            };

            var updateDto = new OperationDto
            {
                Date = new DateOnly(2025, 5, 5),
                Amount = 99.99m,
                Note = "new",
                OperationTypeId = 3
            };

            var updatedEntity = new Operation
            {
                OperationId = existing.OperationId,
                Date = updateDto.Date,
                Amount = updateDto.Amount,
                Note = updateDto.Note,
                OperationTypeId = updateDto.OperationTypeId
            };

            _repoMock.Setup(r => r.GetByIdAsync(existing.OperationId)).ReturnsAsync(existing);
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Operation>())).ReturnsAsync(updatedEntity);

            var result = await _service.UpdateAsync(existing.OperationId, updateDto);

            Assert.NotNull(result);
            Assert.Equal(updatedEntity.OperationId, result.OperationId);
            Assert.Equal(updatedEntity.Date, result.Date);
            Assert.Equal(updatedEntity.Amount, result.Amount);
            Assert.Equal(updatedEntity.Note, result.Note);
            Assert.Equal(updatedEntity.OperationTypeId, result.OperationTypeId);

            _repoMock.Verify(r => r.GetByIdAsync(existing.OperationId), Times.Once);
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Operation>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenNotFound_ReturnsFalse()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Operation?)null);

            var result = await _service.DeleteAsync(123);

            Assert.False(result);
            _repoMock.Verify(r => r.GetByIdAsync(123), Times.Once);
            _repoMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_WhenFound_DeletesAndReturnsTrue()
        {
            var entity = new Operation { OperationId = 55, Date = new DateOnly(2025, 1, 1), Amount = 1m, OperationTypeId = 1 };
            _repoMock.Setup(r => r.GetByIdAsync(entity.OperationId)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.DeleteAsync(entity.OperationId)).ReturnsAsync(true);

            var result = await _service.DeleteAsync(entity.OperationId);

            Assert.True(result);
            _repoMock.Verify(r => r.GetByIdAsync(entity.OperationId), Times.Once);
            _repoMock.Verify(r => r.DeleteAsync(entity.OperationId), Times.Once);
        }
    }
}