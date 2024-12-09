using Moq;
using Xunit;
using LogTransformer.Application.Queries.GetAllTransformedLogs;
using LogTransformer.Core.Repositories;
using LogTransformer.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LogTransformer.Core.Entities;
using System.Linq;

namespace LogTransformer.UnitTeste.Application.Queries
{
    public class GetAllTransformedLogHandlerTests
    {
        private readonly Mock<ITransformedLogRepository> _repositoryMock;
        private readonly GetAllTransformedLogHandler _handler;

        public GetAllTransformedLogHandlerTests()
        {
            _repositoryMock = new Mock<ITransformedLogRepository>();
            _handler = new GetAllTransformedLogHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_TransformedLogs_When_Logs_Exist()
        {
            // Arrange
            var transformedLogs = new List<TransformedLog>
            {
                new TransformedLog("GET", 200, "/api/resource1", 100, 2048, "MISS", "/logs/sample1.log", 1, "Test Provider"),
                new TransformedLog("POST", 201, "/api/resource2", 150, 4096, "HIT", "/logs/sample2.log", 2, "Test Provider")
            };

            int page = 0, size = 10;
            _repositoryMock
                .Setup(repo => repo.GetAllTransformedLogsAsync(page, size))
                .ReturnsAsync(transformedLogs);

            var query = new GetAllTransformedLogQuery(page, size);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count());
            _repositoryMock.Verify(repo => repo.GetAllTransformedLogsAsync(page, size), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Empty_When_No_TransformedLogs_Exist()
        {
            // Arrange
            int page = 0, size = 10;
            _repositoryMock
                .Setup(repo => repo.GetAllTransformedLogsAsync(page, size))
                .ReturnsAsync(new List<TransformedLog>());

            var query = new GetAllTransformedLogQuery(page, size);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data);
            _repositoryMock.Verify(repo => repo.GetAllTransformedLogsAsync(page, size), Times.Once);
        }
    }
}
