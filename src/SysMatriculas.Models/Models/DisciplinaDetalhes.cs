using System.Collections.Generic;

namespace SysMatriculas.Dominio
{
    public class DisciplinaDetalhes
    {
        public int DisciplinaId { get; set; }
        public string Nome { get; set; }
        public bool Aprovado { get; set; }
        public int? Ordem { get; set; }
        public bool TemConexao { get; set; }
        public int? Nota { get; set; }
        public bool TemPreRequisitoNaoConcluido { get; set; }


        public List<int> CoRequisitosIds { get; set; }
        public List<int> PreRequisitosIds { get; set; }
    }
}
