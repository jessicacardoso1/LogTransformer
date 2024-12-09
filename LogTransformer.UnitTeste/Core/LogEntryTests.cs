using Xunit;
using LogTransformer.Core.Entities;

namespace LogTransformer.UnitTeste.Core.Entities
{
    public class LogEntryTests
    {
        [Fact]
        public void Constructor_Should_Set_OriginalContent_Correctly()
        {
            // Arrange
            var originalContent = "Log Content";

            // Act
            var logEntry = new LogEntry(originalContent);

            // Assert
            Assert.Equal(originalContent, logEntry.OriginalContent);
        }
    }
}
