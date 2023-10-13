using Cnab.Api.Domain.NotificationPattern;
using Cnab.Api.Domain.Repositories;
using Cnab.Api.Services;
using Cnab.Api.Test.Mock;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;

namespace Cnab.Api.Test.Services
{
    public class CnabServiceTests
    {
        private readonly Mock<ITransactionTypeRepository> _transactionTypeRepositoryMock;
        private readonly Mock<INotificationContext> _notificationContextMock;
        private readonly Mock<ILogger<CnabService>> _loggerMock;
        private readonly CnabService _cnabService;

        public CnabServiceTests()
        {
            _transactionTypeRepositoryMock = new Mock<ITransactionTypeRepository>();
            _notificationContextMock = new Mock<INotificationContext>();
            _loggerMock = new Mock<ILogger<CnabService>>();
            _cnabService = new CnabService(_transactionTypeRepositoryMock.Object, _notificationContextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task ProccessFileAsync_ProcessesValidFile_ReturnsTransactions()
        {
            var streamContent = @"3201903010000014200096206760174753****3153153453JOÃO MACEDO   BAR DO JOÃO       "; ;
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(streamContent));

            var transactionTypes = TransactionTypesMock.GenerateList();

            _transactionTypeRepositoryMock.Setup(repo => repo.ListAsync()).ReturnsAsync(transactionTypes);

            // Act
            var result = await _cnabService.ProccessFileAsync(stream, Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ProccessFileAsync_ThrowsException_LogsAndReturnsNull()
        {
            // Arrange
            _transactionTypeRepositoryMock.Setup(repo => repo.ListAsync()).Throws<Exception>();
            var streamContent = @"3201903010000014200096206760174753****3153153453JOÃO MACEDO   BAR DO JOÃO       "; ;
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(streamContent));
            // Act
            var result = await _cnabService.ProccessFileAsync(stream, Guid.NewGuid());

            // Assert
            Assert.Null(result);
            _notificationContextMock.Verify(ctx => ctx.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _loggerMock.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}