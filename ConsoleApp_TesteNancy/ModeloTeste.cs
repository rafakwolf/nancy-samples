using FluentValidation;
using System;

namespace ConsoleApp_TesteNancy
{
    public class ModeloTeste
    {
        public string Nome { get; set; }
        public DateTime? Data { get; set; }
    }

    public class ModelTesteValidation : AbstractValidator<ModeloTeste>
    {
        public ModelTesteValidation()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome deve conter alguma informação.");
            RuleFor(x => x.Data)
                .Must(dt =>
                {
                    var isValid = DateTime.TryParse(dt.ToString(), out var validDt);

                    return isValid;
                })
                .WithMessage("Data inválida.");
        }
    }
}
