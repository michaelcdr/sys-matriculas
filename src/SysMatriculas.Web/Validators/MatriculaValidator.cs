using FluentValidation;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Validators
{
    public class MatriculaValidator : AbstractValidator<AssociacaoAlunoComCurso>
    {
        public MatriculaValidator()
        {
            RuleFor(x => x.CurriculoId)
                .NotNull()
                .WithMessage("Selecione o currículo.");

            RuleFor(x => x.UsuarioId)
                .NotNull()
                .WithMessage("Selecione o aluno.");
        }
    }
}
