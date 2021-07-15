namespace SysMatriculas.Dominio.Responses
{
    public class DisciplinaResponse
    {
        public DisciplinaResponse(int disciplinaId, string nome)
        {
            DisciplinaId = disciplinaId;
            Nome = nome;
        }

        public int DisciplinaId { get; set; }
        public string Nome { get; set; }

    }
}
