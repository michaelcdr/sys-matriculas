using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SysMatriculas.Web.ViewModels
{
    public class DisciplinaCadastroViewModel
    {
        public DisciplinaCadastroViewModel(List<SelectListItem> curriculosDisponiveis)
        {
            CurriculosDisponiveis = curriculosDisponiveis;
        }

        public DisciplinaCadastroViewModel()
        {


        }

        public string Nome { get; set; }

        public int? CurriculoId { get; set; }

        public int? CargaHoraria { get; set; }

        public int? Semestre { get; set; }
        public int? Ordem { get; set; }
        public List<int> PreRequisitos { get; set; }
        public List<int> CoRequisitos { get; set; }

        public List<SelectListItem> PreRequisitosDisponiveis { get; set; }
        public List<SelectListItem> CoRequisitosDisponiveis { get; set; }

        public List<SelectListItem> CurriculosDisponiveis { get; set; }
    }
}
