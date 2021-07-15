using System.Collections.Generic;

namespace SysMatriculas.Dominio
{
    public class Disciplina
    {
        public int DisciplinaId { get; set; }

        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int Semestre { get; set; }
        public int? Ordem { get; set; }

        public ICollection<PreRequisito> PreRequisitos { get; private set; }
        public ICollection<PreRequisito> DisciplinasOndeEPreRequisito { get; private set; }

        public ICollection<CoRequisito> CoRequisitos { get; private set; }
        public ICollection<CoRequisito> DisciplinasOndeECoRequisito { get; private set; }

        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }

        public ICollection<Desempenho> Desempenhos { get; set; }

        public Disciplina()
        {
            CoRequisitos = new HashSet<CoRequisito>();
            DisciplinasOndeECoRequisito = new HashSet<CoRequisito>();

            PreRequisitos = new HashSet<PreRequisito>();
            DisciplinasOndeEPreRequisito = new HashSet<PreRequisito>();

            Desempenhos = new HashSet<Desempenho>();
        }


    }
}
