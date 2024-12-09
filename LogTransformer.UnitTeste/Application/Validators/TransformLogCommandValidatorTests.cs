using FluentValidation;
using LogTransformer.Application.Commands.TransformLog;
using LogTransformer.Application.Validators;
using Moq;
using Xunit;

namespace LogTransformer.UnitTeste.Application.Validators
{
    public class TransformLogCommandValidatorTests
    {
        private readonly TransformLogCommandValidator _validator;

        public TransformLogCommandValidatorTests()
        {
            _validator = new TransformLogCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_LogId_Is_Less_Than_One()
        {
            // Arrange
            var command = new TransformLogCommand(logUrl: null, logId: 0, outputFormat: "file");

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "LogId" && e.ErrorMessage == "LogId deve ser maior que 0.");
        }

        [Fact]
        public void Should_Have_Error_When_LogUrl_Is_Missing_And_LogId_Is_Not_Provided()
        {
            // Arrange
            var command = new TransformLogCommand(logUrl: null, logId: null, outputFormat: "file");

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "LogUrl" && e.ErrorMessage == "LogUrl é obrigatório se LogId for informado.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_OutputFormat_Is_Valid()
        {
            // Arrange
            var command = new TransformLogCommand(logUrl: "http://valid.url", logId: null, outputFormat: "file");

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
            Assert.DoesNotContain(result.Errors, e => e.PropertyName == "OutputFormat");
        }

        [Fact]
        public void Should_Integrate_With_Dependent_Validation_Service()
        {
            // Arrange: Simulando um serviço externo para validação de URL
            var mockValidationService = new Mock<IExternalValidationService>();
            mockValidationService.Setup(s => s.IsValidLogUrl(It.IsAny<string>())).Returns(true);

            var command = new TransformLogCommand("http://valid.url", 1,"file");

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
            Assert.DoesNotContain(result.Errors, e => e.PropertyName == "LogUrl");
            mockValidationService.Verify(s => s.IsValidLogUrl(It.IsAny<string>()), Times.Never);
        }
    }

    public interface IExternalValidationService
    {
        bool IsValidLogUrl(string url);
    }
}
