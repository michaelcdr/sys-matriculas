namespace SysMatriculas.Dominio
{
    public class CurriculoRequest
    {
        public CurriculoRequest(int curriculoId, string nome)
        {
            CurriculoId = curriculoId;
            Nome = nome;
        }

        public int CurriculoId { get; set; }
        public string Nome { get; set; }
    }
}
