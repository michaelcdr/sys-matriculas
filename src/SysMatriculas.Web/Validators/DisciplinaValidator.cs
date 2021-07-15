using FluentValidation;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Validators
{
    public class DisciplinaValidator : AbstractValidator<DisciplinaCadastroViewModel>
    {
        public DisciplinaValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                    .WithMessage("Nome obrigatório.");

            RuleFor(x => x.CurriculoId)
                .NotNull()
                    .WithMessage("Currículo obrigatório.");

            RuleFor(x => x.CargaHoraria)
                .NotNull()
                    .WithMessage("Carga horária obrigatória.");

            RuleFor(x => x.Semestre)
                .NotNull()
                    .WithMessage("Semestre obrigatório.");
        }
    }
}
