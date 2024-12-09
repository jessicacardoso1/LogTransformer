using Moq;
using Xunit;
using LogTransformer.Application.Commands.InsertLogEntry;
using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using LogTransformer.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LogTransformer.UnitTeste.Application.Commands.InsertLogEntry
{
    public class InsertLogEntryHandlerTests
    {
        private readonly Mock<ILogEntryRepository> _logEntryRepositoryMock;
        private readonly Mock<ITransformedLogRepository> _transformedLogRepositoryMock;
        private readonly InsertLogEntryHandler _handler;

        public InsertLogEntryHandlerTests()
        {
            // Configura os mocks
            _logEntryRepositoryMock = new Mock<ILogEntryRepository>();
            _transformedLogRepositoryMock = new Mock<ITransformedLogRepository>();

            // Cria o handler com os mocks
            _handler = new InsertLogEntryHandler(_logEntryRepositoryMock.Object, _transformedLogRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Save_Log_Entry_And_Return_Success()
        {
            // Arrange
            var command = new InsertLogEntryCommand
            {
                OriginalContent = "\"312|200|HIT|\\\"GET /robots.txt HTTP/1.1\\\"|100.2\\r\\n101|200|MISS|\r\n"
            };

            var logEntry = new LogEntry(command.OriginalContent);

            _logEntryRepositoryMock
                .Setup(repo => repo.SaveLogAsync(It.IsAny<LogEntry>()))  // Configura o mock para retornar um Task<int>
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            // Verifica se o método SaveLogAsync foi chamado
            _logEntryRepositoryMock.Verify(repo => repo.SaveLogAsync(It.Is<LogEntry>(x => x.OriginalContent == command.OriginalContent)), Times.Once);

            // Verifica se o retorno é um sucesso com o ID correto
            Assert.True(result.IsSuccess);
            Assert.True(result.Data >= 0);
        }
    }
}
