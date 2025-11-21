using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SFMB.DAL;
using SFMB.DAL.Entities;
using SFMB.DAL.Repositories;

namespace SFMB.Tests
{
    public class OperationRepositoryTests : IDisposable
    {
        private readonly SfmbDbContext _context;
        private readonly OperationRepository _repository;

        public OperationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SfmbDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var configuration = new ConfigurationBuilder().Build();
            _context = new SfmbDbContext(options, configuration);
            _repository = new OperationRepository(_context);
        }

        [Fact]
        public async Task AddAsync_PersistsEntity()
        {
            var op = new Operation
            {
                Date = new DateOnly(2025, 6, 6),
                Amount = 15.5m,
                Note = "repo add",
                OperationTypeId = 1
            };

            var created = await _repository.AddAsync(op);

            Assert.True(created.OperationId != 0);
            Assert.Equal(1, _context.Operations.Count());
            var persisted = _context.Operations.First();
            Assert.Equal(op.Note, persisted.Note);
            Assert.Equal(op.Amount, persisted.Amount);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEntityOrNull()
        {
            var op = new Operation { Date = new DateOnly(2025, 1, 1), Amount = 1m, Note = "get", OperationTypeId = 1 };
            _context.Operations.Add(op);
            await _context.SaveChangesAsync();

            var fetched = await _repository.GetByIdAsync(op.OperationId);
            Assert.NotNull(fetched);
            Assert.Equal(op.OperationId, fetched.OperationId);

            var notFound = await _repository.GetByIdAsync(9999);
            Assert.Null(notFound);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOrderedList()
        {
            var older = new Operation { Date = new DateOnly(2020, 1, 1), Amount = 1m, Note = "older", OperationTypeId = 1 };
            var newer = new Operation { Date = new DateOnly(2025, 1, 1), Amount = 2m, Note = "newer", OperationTypeId = 1 };
            _context.Operations.AddRange(older, newer);
            await _context.SaveChangesAsync();

            var all = (await _repository.GetAllAsync()).ToList();

            Assert.Equal(2, all.Count);
            // Repository orders by Date descending
            Assert.Equal(newer.Date, all[0].Date);
            Assert.Equal(older.Date, all[1].Date);
        }

        [Fact]
        public async Task UpdateAsync_PersistsChanges()
        {
            var op = new Operation { Date = new DateOnly(2024, 1, 1), Amount = 10m, Note = "before", OperationTypeId = 1 };
            _context.Operations.Add(op);
            await _context.SaveChangesAsync();

            op.Note = "after";
            op.Amount = 99.99m;

            var updated = await _repository.UpdateAsync(op);

            Assert.Equal("after", updated.Note);
            Assert.Equal(99.99m, updated.Amount);

            var persisted = await _context.Operations.FindAsync(op.OperationId);
            Assert.Equal("after", persisted.Note);
        }

        [Fact]
        public async Task DeleteAsync_RemovesWhenExists_ReturnsTrue_OtherwiseFalse()
        {
            var op = new Operation { Date = new DateOnly(2025, 5, 5), Amount = 5m, Note = "to delete", OperationTypeId = 1 };
            _context.Operations.Add(op);
            await _context.SaveChangesAsync();

            var result = await _repository.DeleteAsync(op.OperationId);
            Assert.True(result);
            Assert.Empty(_context.Operations);

            var resultFalse = await _repository.DeleteAsync(9999);
            Assert.False(resultFalse);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}