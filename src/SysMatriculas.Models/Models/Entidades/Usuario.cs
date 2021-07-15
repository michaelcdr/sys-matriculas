using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SysMatriculas.Dominio
{
    public class Usuario : IdentityUser
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public bool Ativo { get; set; }
        public string Sexo { get; set; }

        public ICollection<Matricula> Matriculas { get; set; }
        public ICollection<Desempenho> Desempenhos { get; set; }
    }
}