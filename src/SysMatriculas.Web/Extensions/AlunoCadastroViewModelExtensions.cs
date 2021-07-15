using SysMatriculas.Dominio;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Extensions
{
    public static class AlunoCadastroViewModelExtensions
    {
        public static UsuarioRequest ToUsuarioRequest(this AlunoCadastroViewModel aluno)
        {
            return new UsuarioRequest(
                aluno.Nome,
                aluno.Sobrenome,
                aluno.Email,
                aluno.Senha,
                aluno.Login,
                aluno.CPF,
                aluno.Sexo
            );
        }
    }
}
