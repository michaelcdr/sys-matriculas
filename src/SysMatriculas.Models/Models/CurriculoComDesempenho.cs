using System.Collections.Generic;

namespace SysMatriculas.Dominio.Models
{
    public class CurriculoComDesempenho
    {
        public CurriculoComDesempenho(int curriculoId, string nome)
        {
            CurriculoId = curriculoId;
            Nome = nome;
            Disciplinas = new List<DiscplinaComDesempenho>();
        }

        public int CurriculoId { get; set; }
        public string Nome { get; set; }
        public List<DiscplinaComDesempenho> Disciplinas { get; set; }
    }
}
