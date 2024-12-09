using Moq;
using Xunit;
using LogTransformer.Application.Queries.GetLogEntryById;
using LogTransformer.Core.Repositories;
using LogTransformer.Application.Models;
using System.Threading;
using System.Threading.Tasks;
using LogTransformer.Core.Entities;

namespace LogTransformer.UnitTeste.Application.Queries
{
    public class GetLogEntryByIdHandlerTests
    {
        private readonly Mock<ILogEntryRepository> _repositoryMock;
        private readonly GetLogEntryByIdHandler _handler;

        public GetLogEntryByIdHandlerTests()
        {
            _repositoryMock = new Mock<ILogEntryRepository>();
            _handler = new GetLogEntryByIdHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_LogEntry_When_Log_Exists()
        {
            // Arrange
            var logEntry = new LogEntry("Log Content") { Id = 1 };

            _repositoryMock
                .Setup(repo => repo.GetLogByIdAsync(1))
                .ReturnsAsync(logEntry);

            var query = new GetLogEntryByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Log Content", result.Data.OriginalContent);
            _repositoryMock.Verify(repo => repo.GetLogByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_Log_Does_Not_Exist()
        {
            // Arrange
            _repositoryMock
                .Setup(repo => repo.GetLogByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((LogEntry)null);

            var query = new GetLogEntryByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("O Log Transformado não existe.", result.Message);
            Assert.Null(result.Data);
            _repositoryMock.Verify(repo => repo.GetLogByIdAsync(1), Times.Once);
        }
    }
}
