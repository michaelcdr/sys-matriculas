using SysMatriculas.Dominio;
using SysMatriculas.Web.Models;

namespace SysMatriculas.Web.Extensions
{
    public static class UsuarioViewModelExtensions
    {
        public static UsuarioRequest ToUsuarioRequest(this UsuarioCadastro viewModel)
        {
            return new UsuarioRequest(
                viewModel.Nome,
                viewModel.Sobrenome,
                viewModel.Email,
                viewModel.Senha,
                viewModel.Login,
                viewModel.CPF,
                viewModel.Sexo
            );
        }
    }
}
