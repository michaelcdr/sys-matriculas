using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Web.Helpers;

public interface ISelectListItemHelper
{
    Task<List<SelectListItem>> ObterCurriculosSelectList();
    Task<List<SelectListItem>> ObterSelectListDisciplinas(int curriculoId, int? disciplinaAtual);

    Task<List<SelectListItem>> ObterAlunos();
    Task<List<SelectListItem>> ObterCursos();
}
