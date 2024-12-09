using Moq;
using Xunit;
using LogTransformer.Application.Queries.GetTransformedLogById;
using LogTransformer.Core.Repositories;
using LogTransformer.Application.Models;
using System.Threading;
using System.Threading.Tasks;
using LogTransformer.Core.Entities;

namespace LogTransformer.UnitTests.Application.Queries
{
    public class GetTransformedLogByIdHandlerTests
    {
        private readonly Mock<ITransformedLogRepository> _repositoryMock;
        private readonly GetTransformedLogByIdHandler _handler;

        public GetTransformedLogByIdHandlerTests()
        {
            _repositoryMock = new Mock<ITransformedLogRepository>();
            _handler = new GetTransformedLogByIdHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Log_When_Log_Exists()
        {
            // Arrange
            var log = new TransformedLog(
                httpMethod: "GET",
                statusCode: 200,
                uriPath: "/api/resource",
                timeTaken: 100,
                responseSize: 2048,
                cacheStatus: "MISS",
                filePath: "/logs/sample.log",
                originalLogId: 1,
                provider: "Test Provider")
            {
                Id = 1
            };

            _repositoryMock
                .Setup(repo => repo.GetTransformedLogByIdAsync(1))
                .ReturnsAsync(log);

            var query = new GetTransformedLogByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("GET", result.Data.HttpMethod);
            _repositoryMock.Verify(repo => repo.GetTransformedLogByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_Log_Does_Not_Exist()
        {
            // Arrange
            _repositoryMock
                .Setup(repo => repo.GetTransformedLogByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((TransformedLog)null);

            var query = new GetTransformedLogByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("O Log Transformado não existe.", result.Message);
            Assert.Null(result.Data);
            _repositoryMock.Verify(repo => repo.GetTransformedLogByIdAsync(1), Times.Once);
        }
    }
}
