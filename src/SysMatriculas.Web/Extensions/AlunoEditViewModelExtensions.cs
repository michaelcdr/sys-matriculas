using SysMatriculas.Dominio;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Extensions
{
    public static class AlunoEditViewModelExtensions
    {
        public static UsuarioRequest ToUsuarioRequest(this AlunoEditViewModel aluno)
        {
            return new UsuarioRequest(
                aluno.Nome,
                aluno.Sobrenome,
                aluno.Email,
                null,
                aluno.Login,
                aluno.CPF,
                aluno.Sexo
            );
        }
    }
}
