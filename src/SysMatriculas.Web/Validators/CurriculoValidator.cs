using FluentValidation;
using SysMatriculas.Dominio;

namespace SysMatriculas.Web.Validators
{
    public class CurriculoValidator
        : AbstractValidator<Curriculo>
    {
        public CurriculoValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("Nome obrigatório.");

            RuleFor(x => x.CursoId)
                .NotNull()
                .WithMessage("Curso obrigatório.");
        }
    }
}
