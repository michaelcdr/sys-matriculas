using FluentValidation;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Validators
{
    public class AlunoEditValidator : AbstractValidator<AlunoEditViewModel>
    {
        public AlunoEditValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("Nome obrigatório.");

            RuleFor(x => x.Sobrenome)
                .NotNull()
                .WithMessage("Sobrenome obrigatório.");

            RuleFor(x => x.Email)
                .NotNull()
                    .WithMessage("E-mail obrigatório.")
                .EmailAddress()
                    .WithMessage("E-mail inválido");

            RuleFor(x => x.Login)
                .NotNull()
                    .WithMessage("Login obrigatório.");
        }
    }
}
