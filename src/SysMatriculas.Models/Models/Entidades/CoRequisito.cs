namespace SysMatriculas.Dominio
{
    public class CoRequisito
    {
        public int CoRequisitosId { get; set; }

        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }

        public int DisciplinaPaiId { get; set; }
        public Disciplina DisciplinaPai { get; set; }

        public CoRequisito()
        {

        }

        public CoRequisito(int disciplinaId)
        {
            DisciplinaId = disciplinaId;
        }
    }
}
