using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SysMatriculas.Web.ViewModels
{
    public class CoordenadorEdit
    {
        public string UsuarioId { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }

        public List<SelectListItem> SexosDisponiveis
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Feminino","Feminino"),
                    new SelectListItem("Masculino","Masculino")
                };
            }
        }
    }
}
