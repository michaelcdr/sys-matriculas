namespace SysMatriculas.Dominio
{
    public class ConcluirDisciplinaRequest
    {
        public ConcluirDisciplinaRequest(string usuarioId, int disciplinaId, int nota, bool aprovado)
        {
            UsuarioId = usuarioId;
            DisciplinaId = disciplinaId;
            Nota = nota;
            Aprovado = aprovado;
        }

        public string UsuarioId { get; set; }
        public int DisciplinaId { get; set; }
        public int Nota { get; set; }
        public bool Aprovado { get; set; }
    }
}
