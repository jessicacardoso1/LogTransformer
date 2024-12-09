using FluentAssertions;
using LogTransformer.Application.Commands.InsertLogEntry;
using LogTransformer.Application.Validators;
using System;
using Xunit;

namespace LogTransformer.UnitTeste.Application.Validators
{
    public class InsertLogEntryValidatorTests
    {
        private readonly InsertLogEntryValidator _validator;

        public InsertLogEntryValidatorTests()
        {
            _validator = new InsertLogEntryValidator();
        }

        [Fact]
        public void Validate_QuandoLogEstaNoFormatoCorreto_DeveSerValido()
        {
            // Arrange
            var command = new InsertLogEntryCommand
            {
                OriginalContent = "312|200|HIT|\\\"GET /robots.txt HTTP/1.1\\\"|100.2"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_QuandoLogEstaInvalido_DeveRetornarErro()
        {
            // Arrange
            var command = new InsertLogEntryCommand
            {
                OriginalContent = "string"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "O conteúdo do log deve ter pelo menos 10 caracteres.");
        }
    }

}
