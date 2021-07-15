namespace SysMatriculas.Web.ViewModels
{
    public class AlunoDesempenhoViewModel
    {
        public int CurriculoId { get; set; }
        public string NomeDoCurso { get;  set; }

        public AlunoDesempenhoViewModel(int curriculoId)
        {
            CurriculoId = curriculoId;
        }
    }
}
