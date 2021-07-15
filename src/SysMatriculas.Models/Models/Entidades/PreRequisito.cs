namespace SysMatriculas.Dominio
{
    public class PreRequisito
    {
        public int PreRequisitosId { get; set; }

        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }

        public int DisciplinaPaiId { get; set; }
        public Disciplina DisciplinaPai { get; set; }

        public PreRequisito()
        {

        }

        public PreRequisito(int disciplinaId)
        {
            DisciplinaId = disciplinaId;
        }
    }
}
