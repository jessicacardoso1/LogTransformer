using System;
using FluentValidation;
using LogTransformer.Application.Commands.InsertLogEntry;
using LogTransformer.Application.Commands.TransformLog;

namespace LogTransformer.Application.Validators
{
    public class InsertLogEntryValidator : AbstractValidator<InsertLogEntryCommand>
    {
        public InsertLogEntryValidator()
        {
            RuleFor(x => x.OriginalContent)
                .NotEmpty().WithMessage("O conteúdo do log não pode ser vazio.")
                .MinimumLength(10).WithMessage("O conteúdo do log deve ter pelo menos 10 caracteres.");
        }
    }
}
