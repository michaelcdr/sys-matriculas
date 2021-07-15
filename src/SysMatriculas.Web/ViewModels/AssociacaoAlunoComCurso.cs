using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SysMatriculas.Web.ViewModels
{
    public class AssociacaoAlunoComCurso
    {
        public int CurriculoId { get; set; }
        public string UsuarioId { get; set; }
        public List<SelectListItem> CurriculosDisponiveis { get; set; }
        public List<SelectListItem> AlunosDisponiveis { get; set; }
    }
}
