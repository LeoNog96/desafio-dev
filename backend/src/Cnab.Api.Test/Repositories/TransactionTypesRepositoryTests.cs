using Cnab.Api.Data.Context;
using Cnab.Api.Data.Repositories;
using Cnab.Api.Domain.Cache;
using Cnab.Api.Domain.Entities;
using Cnab.Api.Test.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Cnab.Api.Test.Repositories
{
    public class TransactionTypeRepositoryTests
    {
        private readonly CnabContext _dbContext;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly TransactionTypeRepository _repository;

        public TransactionTypeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CnabContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase{Guid.NewGuid()}") // Using a unique name to prevent sharing state between tests
                .Options;

            _dbContext = new CnabContext(options);
            _cacheServiceMock = new Mock<ICacheService>();
            var logger = new Logger<TransactionTypes>(new NullLoggerFactory());
            _repository = new TransactionTypeRepository(_dbContext, logger, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task ListAsync_ReturnsDataFromCache_IfAvailable()
        {
            // Arrange
            var transactionTypes = TransactionTypesMock.GenerateList();
            _cacheServiceMock.Setup(cs => cs.GetEnumerableCacheAsync<TransactionTypes>(It.IsAny<string>()))
                .ReturnsAsync(transactionTypes);

            // Act
            var result = await _repository.ListAsync();

            // Assert
            Assert.Equal(transactionTypes, result);
        }

        [Fact]
        public async Task ListAsync_FetchesFromDbAndSetsCache_IfCacheEmpty()
        {
            // Arrange
            _dbContext.TransactionTypes.Add(TransactionTypesMock.Generate());
            _dbContext.SaveChanges();

            _cacheServiceMock.Setup(cs => cs.GetEnumerableCacheAsync<TransactionTypes>(It.IsAny<string>()))
                .ReturnsAsync((IEnumerable<TransactionTypes>)null);
            _cacheServiceMock.Setup(cs => cs.SetEnumerableCacheAsync(It.IsAny<IEnumerable<TransactionTypes>>(), It.IsAny<TimeSpan>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _repository.ListAsync();

            // Assert
            Assert.NotEmpty(result);
            _cacheServiceMock.Verify(cs => cs.SetEnumerableCacheAsync(It.IsAny<IEnumerable<TransactionTypes>>(), It.IsAny<TimeSpan>(), It.IsAny<string>()), Times.Once);
        }
    }
}
