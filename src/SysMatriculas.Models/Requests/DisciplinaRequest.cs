using System.Collections.Generic;

namespace SysMatriculas.Dominio
{
    public class DisciplinaRequest
    {
        public int? DisciplinaId { get; set; }
        public string Nome { get; set; }
        public int CurriculoId { get; set; }
        public int CargaHoraria { get; set; }
        public int Semestre { get; set; }
        public List<int> PreRequisitos { get; set; }
        public List<int> CoRequisitos { get; set; }
        public int? Ordem { get; set; }

        public DisciplinaRequest()
        {
            PreRequisitos = new List<int>();
            CoRequisitos = new List<int>();
        }

        public DisciplinaRequest(string nome, int curriculoId, int cargaHoraria, int semestre, List<int> preRequisitos, List<int> coRequisitos, int? ordem)
        {
            Nome = nome;
            CurriculoId = curriculoId;
            CargaHoraria = cargaHoraria;
            Semestre = semestre;
            PreRequisitos = preRequisitos;
            CoRequisitos = coRequisitos;
            Ordem = ordem;
        }
    }
}
