using System.Collections.Generic;

namespace SysMatriculas.Dominio
{
    public class SemestreComDisciplinas
    {
        public int Ordem { get; set; }
        public string Nome { get; set; }
        public List<DisciplinaDetalhes> Disciplinas { get; set; }

        public SemestreComDisciplinas()
        {
            Disciplinas = new List<DisciplinaDetalhes>();
        }
    }
}
