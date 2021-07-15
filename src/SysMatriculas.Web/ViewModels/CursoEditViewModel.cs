using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using System.Collections.Generic;

namespace SysMatriculas.Web.ViewModels
{
    public class CursoEditViewModel : CursoViewModel
    {
        public int CursoId { get; set; }

        public List<CurriculoResponse> CurriculosAtuais { get; set; }

        public List<CurriculoResponse> CurriculosPossiveis { get; set; }
    }
}
