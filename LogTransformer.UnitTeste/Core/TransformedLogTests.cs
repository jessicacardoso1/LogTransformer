using Xunit;
using LogTransformer.Core.Entities;

namespace LogTransformer.UnitTeste.Core.Entities
{
    public class TransformedLogTests
    {
        [Fact]
        public void Constructor_Should_Set_Properties_Correctly()
        {
            // Arrange
            var httpMethod = "GET";
            var statusCode = 200;
            var uriPath = "/api/resource";
            var timeTaken = 100;
            var responseSize = 2048;
            var cacheStatus = "MISS";
            var filePath = "/logs/sample.log";
            var originalLogId = 1;
            var provider = "MINHA CDN";

            // Act
            var transformedLog = new TransformedLog(httpMethod, statusCode, uriPath, timeTaken, responseSize, cacheStatus, filePath, originalLogId, provider);

            // Assert
            Assert.Equal(httpMethod, transformedLog.HttpMethod);
            Assert.Equal(statusCode, transformedLog.StatusCode);
            Assert.Equal(uriPath, transformedLog.UriPath);
            Assert.Equal(timeTaken, transformedLog.TimeTaken);
            Assert.Equal(responseSize, transformedLog.ResponseSize);
            Assert.Equal(cacheStatus, transformedLog.CacheStatus);
            Assert.Equal(filePath, transformedLog.FilePath);
            Assert.Equal(originalLogId, transformedLog.OriginalLogId);
            Assert.Equal(provider, transformedLog.Provider);
        }

        [Fact]
        public void SetFilePath_Should_Update_FilePath()
        {
            // Arrange
            var transformedLog = new TransformedLog("GET", 200, "/api/resource", 100, 2048, "MISS", "/logs/sample.log", 1);
            var newFilePath = "/logs/new_sample.log";

            // Act
            transformedLog.SetFilePath(newFilePath);

            // Assert
            Assert.Equal(newFilePath, transformedLog.FilePath);
        }
    }
}
