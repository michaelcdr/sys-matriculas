namespace SysMatriculas.Dominio.Requests
{
    public class DesempenhoAlunoRequest
    {
        public DesempenhoAlunoRequest(string usuarioId, int disciplinaId)
        {
            UsuarioId = usuarioId;
            DisciplinaId = disciplinaId;
        }

        public string UsuarioId { get; set; }
        public int DisciplinaId { get; set; }
    }
}
