using Cnab.Api.Data.Context;
using Cnab.Api.Data.Repositories;
using Cnab.Api.Domain.Cache;
using Cnab.Api.Domain.Entities;
using Cnab.Api.Test.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cnab.Api.Test.Repositories
{
    public class UserRepositoryTests
    {
        private readonly CnabContext _dbContext;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CnabContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_User")
                .Options;

            _dbContext = new CnabContext(options);
            _cacheServiceMock = new Mock<ICacheService>();
            var logger = new Logger<User>(new NullLoggerFactory());
            _repository = new UserRepository(_dbContext, logger, _cacheServiceMock.Object);
        }

        // ...
        [Fact]
        public async Task CreateAsync_AddsUserAndInvalidatesCache()
        {
            // Arrange
            var user = UserMock.Generate();

            _cacheServiceMock.Setup(cs => cs.DeleteKeysByPrefixAsync<User>())
                .Returns(Task.CompletedTask);

            // Act
            await _repository.CreateAsync(user);

            // Assert
            var addedUser = await _dbContext.Users.FindAsync(user.Id);
            Assert.NotNull(addedUser);
            _cacheServiceMock.Verify(cs => cs.DeleteKeysByPrefixAsync<User>(), Times.Once);
        }

        [Fact]
        public async Task GetbyLoginAsync_ReturnsCorrectUser()
        {
            // Arrange
            var user = UserMock.Generate();
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Act
            var returnedUser = await _repository.GetbyLoginAsync(user.Login);

            // Assert
            Assert.NotNull(returnedUser);
            Assert.Equal(user.Login, returnedUser.Login);
        }

    }
}
