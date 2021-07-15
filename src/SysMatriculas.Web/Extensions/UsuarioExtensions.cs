using SysMatriculas.Dominio;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Extensions
{
    public static class UsuarioExtensions
    {
        public static AlunoEditViewModel ToEditViewModel(this Usuario usuario)
        {
            return new AlunoEditViewModel
            {
                CPF = usuario.CPF,
                Email = usuario.Email,
                Sexo = usuario.Sexo,
                Login = usuario.UserName,
                Nome = usuario.Nome,
                Sobrenome = usuario.SobreNome,
                UsuarioId = usuario.Id
            };
        }

        public static CoordenadorEdit ToCoordenadorEdit(this Usuario usuario)
        {
            return new CoordenadorEdit
            {
                CPF = usuario.CPF,
                Email = usuario.Email,
                Sexo = usuario.Sexo,
                Login = usuario.UserName,
                Nome = usuario.Nome,
                Sobrenome = usuario.SobreNome,
                UsuarioId = usuario.Id
            };
        }
            
    }
}
