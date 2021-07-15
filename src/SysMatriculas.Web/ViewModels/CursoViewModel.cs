using Microsoft.AspNetCore.Mvc.Rendering;
using SysMatriculas.Dominio.Requests;
using System.Collections.Generic;

namespace SysMatriculas.Web.ViewModels
{
    public class CursoViewModel
    {
        public string Nome { get; set; }
        public string[] Turno { get; set; }
        //public List<int> Curriculos { get; set; }
        public List<string> Curriculos { get; set; }

        public List<SelectListItem> TurnoItens
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Manhã", "Manhã"),
                    new SelectListItem("Vespertino", "Vespertino"),
                    new SelectListItem("Noite", "Noite")
                };
            }
        }
    }
}
