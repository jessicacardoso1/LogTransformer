using Moq;
using Xunit;
using LogTransformer.Core.Repositories;
using LogTransformer.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LogTransformer.Core.Entities;
using System.Linq;
using LogTransformer.Application.Queries.GetTransformedLogById;

namespace LogTransformer.UnitTeste.Application.Queries
{
    public class GetTransformedLogByLogIdHandlerTests
    {
        private readonly Mock<ITransformedLogRepository> _repositoryMock;
        private readonly GetTransformedLogByLogIdHandler _handler;

        public GetTransformedLogByLogIdHandlerTests()
        {
            _repositoryMock = new Mock<ITransformedLogRepository>();
            _handler = new GetTransformedLogByLogIdHandler(_repositoryMock.Object);
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

            _repositoryMock
                .Setup(repo => repo.GetTransformedLogsByLogIdAsync(It.IsAny<int>()))
                .ReturnsAsync(transformedLogs);

            var query = new GetTransformedLogByLogIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            _repositoryMock.Verify(repo => repo.GetTransformedLogsByLogIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_No_Logs_Exist()
        {
            // Arrange
            _repositoryMock
                .Setup(repo => repo.GetTransformedLogsByLogIdAsync(It.IsAny<int>()))
                .ReturnsAsync((List<TransformedLog>)null);

            var query = new GetTransformedLogByLogIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("O Log Transformado não existe.", result.Message);
            Assert.Null(result.Data);
            _repositoryMock.Verify(repo => repo.GetTransformedLogsByLogIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
