using SysMatriculas.Dominio;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Extensions
{
    public static class CursoExtensions
    {
        public static CursoEditViewModel ToCursoEditViewModel(this Curso curso)
        {
            return new CursoEditViewModel
            {
                Nome = curso.Nome,
                CursoId = curso.CursoId,
                Turno = curso.Turno.Split(',')

            };
        }
    }
}
