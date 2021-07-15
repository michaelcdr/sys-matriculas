using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Web.ViewModels;
using System.Linq;

namespace SysMatriculas.Web.Extensions
{
    public static class CursoEditViewModelExtensions
    {
        public static CursoEditRequest ToCursoEditRequest(this CursoEditViewModel viewModel)
        {
            return new CursoEditRequest
            {
                Nome = viewModel.Nome,
                CursoId = viewModel.CursoId,
                Turno = viewModel.Turno,
                Curriculos = viewModel.Curriculos
            };

        }
    }
}
