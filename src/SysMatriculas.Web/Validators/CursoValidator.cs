using FluentValidation;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Validators
{
    public class CursoValidator
        : AbstractValidator<CursoViewModel>
    {
        public CursoValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                    .WithMessage("Nome obrigatório.");

            RuleFor(x => x.Turno)
                .NotNull()
                    .WithMessage("Turno obrigatório.");

            RuleFor(x => x.Curriculos)
                .Must(e => e != null && e.Count > 0)
                    .WithMessage("Selecione um currículo.");
        }
    }
}
