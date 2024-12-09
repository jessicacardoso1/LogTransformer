using Moq;
using Xunit;
using LogTransformer.Application.Queries.GetAllLogsEntry;
using LogTransformer.Core.Repositories;
using LogTransformer.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LogTransformer.Core.Entities;
using System.Linq;

public class GetAllLogEntryHandlerTests
{
    private readonly Mock<ILogEntryRepository> _repositoryMock;
    private readonly GetAllLogEntryHandler _handler;

    public GetAllLogEntryHandlerTests()
    {
        _repositoryMock = new Mock<ILogEntryRepository>();
        _handler = new GetAllLogEntryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_LogEntries_When_Logs_Exist()
    {
        // Arrange
        var logEntries = new List<LogEntry>
        {
            new LogEntry("Log 1"),
            new LogEntry("Log 2")
        };

        int page = 0, size = 10;
        _repositoryMock
            .Setup(repo => repo.GetAllLogsAsync(page, size))
            .ReturnsAsync(logEntries);

        var query = new GetAllLogsEntryQuery(page, size);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count());
        _repositoryMock.Verify(repo => repo.GetAllLogsAsync(page, size), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Empty_When_No_Logs_Exist()
    {
        // Arrange
        int page = 0, size = 10;
        _repositoryMock
            .Setup(repo => repo.GetAllLogsAsync(page, size))
            .ReturnsAsync(new List<LogEntry>());

        var query = new GetAllLogsEntryQuery(page, size);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Data);
        _repositoryMock.Verify(repo => repo.GetAllLogsAsync(page, size), Times.Once);
    }
}
