using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.DTO
{
    public class DisciplinaDTO
    {
        public int DisciplinaId { get; set; }
        public string Nome { get; set; }
        public string Curriculo { get; set; }
        public int? CurriculoId { get; set; }
    }
}
