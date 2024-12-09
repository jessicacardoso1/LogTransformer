using FluentValidation;
using LogTransformer.Application.Commands.TransformLog;
using System;

public class TransformLogCommandValidator : AbstractValidator<TransformLogCommand>
{
    public TransformLogCommandValidator()
    {
      
        RuleFor(x => x.LogId)
            .GreaterThan(0).When(x => x.LogId.HasValue)
            .WithMessage("LogId deve ser maior que 0.");

        RuleFor(x => x.LogUrl)
            .NotEmpty().When(x => !x.LogId.HasValue) 
            .WithMessage("LogUrl é obrigatório se LogId for informado.");


        RuleFor(x => x.OutputFormat)
            .NotEmpty()
            .WithMessage("OutputFormat é obrigatório.")
            .Must(format => format == "file" || format == "content")
            .WithMessage("OutputFormat deve ser 'file' ou 'content'.");
    }
}
