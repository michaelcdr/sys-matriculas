using System.Collections.Generic;

namespace SysMatriculas.Dominio.Requests
{
    public class DesempenhoDisciplinasAlunoRequest
    {
        public DesempenhoDisciplinasAlunoRequest(List<int> disciplinasIds, string usuarioId)
        {
            DisciplinasIds = disciplinasIds;
            UsuarioId = usuarioId;
        }

        public List<int> DisciplinasIds { get; set; }
        public string UsuarioId { get; set; }
    }
}
