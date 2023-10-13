using Cnab.Api.Data.Context;
using Cnab.Api.Data.Repositories;
using Cnab.Api.Domain.Cache;
using Cnab.Api.Domain.Dtos;
using Cnab.Api.Domain.Entities;
using Cnab.Api.Test.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cnab.Api.Tests.Repositories
{
    public class TransactionRepositoryTests
    {
        private readonly Mock<ICacheService> _mockCacheService;
        private readonly Mock<ILogger<Transaction>> _mockLogger;
        private readonly CnabContext _dbContext;
        private readonly TransactionRepository _repository;
        private readonly Guid _userId;
        public TransactionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CnabContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var user = UserMock.Generate();
            _userId = user.Id;
            
            _dbContext = new CnabContext(options);
            _dbContext.TransactionTypes.AddRange(TransactionTypesMock.GenerateList());
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

           _mockCacheService = new Mock<ICacheService>();
            _mockLogger = new Mock<ILogger<Transaction>>();
            _repository = new TransactionRepository(_dbContext, _mockLogger.Object, _mockCacheService.Object);
        }

        [Fact]
        public async Task CreateRangeAsync_ShouldInsertTransactionsAndInvalidateCache()
        {
            var transactions = new List<Transaction> {
                TransactionMock.CreateFakeTransaction(_userId),
                TransactionMock.CreateFakeTransaction(_userId)
            };
            var result = await _repository.CreateRangeAsync(transactions);

            Assert.Equal(transactions.Count, result.Count());

            _mockCacheService.Verify(cache =>
                cache.DeleteKeysByPrefixAsync<ListTransactionPerStoreDto>(), Times.Once);
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ShouldReturnCorrectTransaction()
        {
            var transaction = TransactionMock.CreateFakeTransaction(_userId);
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetTransactionByIdAsync(transaction.Id);

            Assert.Equal(transaction.Id, result.Id);
        }

        [Fact]
        public async Task ListPaginateAsync_ShouldReturnPaginatedResult()
        {
            // Adicionando algumas transações de exemplo
            _dbContext.Transactions.AddRange(
                TransactionMock.CreateFakeTransaction(_userId),
                TransactionMock.CreateFakeTransaction(_userId),
                TransactionMock.CreateFakeTransaction(_userId)
            );
            await _dbContext.SaveChangesAsync();

            var filterDto = new PaginateTransactionFilterDto
            {
                PageNumber = 1,
                PageSize = 2
            };

            var result = await _repository.ListPaginateAsync(filterDto);

            Assert.Equal(2, result.Data.Count());
        }   
    }
}
