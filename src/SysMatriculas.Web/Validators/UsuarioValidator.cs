using FluentValidation;
using SysMatriculas.Web.Models;

namespace SysMatriculas.Web.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioCadastro>
    {
        public UsuarioValidator()
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

            RuleFor(x => x.Senha)
                .NotNull()
                    .WithMessage("Senha obrigatória.")
                .Equal(e => e.SenhaConfirmacao)
                    .WithMessage("Confirme a senha");


            RuleFor(x => x.SenhaConfirmacao)
              .NotNull()
                  .WithMessage("Confirmação de Senha obrigatória.")
              .Equal(e => e.Senha)
                  .WithMessage("Confirme a senha");

          
        }
    }
}
