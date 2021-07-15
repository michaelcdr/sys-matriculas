namespace SysMatriculas.Dominio.Responses
{
    public class CurriculoResponse
    {
        public CurriculoResponse(int curriculoId, string nome)
        {
            CurriculoId = curriculoId;
            Nome = nome;
        }

        public int CurriculoId { get; set; }
        public string Nome { get; set; }
    }
}
