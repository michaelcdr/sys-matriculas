using System.Collections.Generic;

namespace SysMatriculas.Dominio
{
    public class CursoRequest
    {
        public CursoRequest(string nome, string[] turno, List<string> curriculos)
        {
            Nome = nome;
            Curriculos = curriculos;
            Turno = turno;
        }
        public CursoRequest()
        {

        }

        public string Nome { get; set; }
        public List<string> Curriculos { get; set; }
        public string[] Turno { get; set; }
    }

    public class CursoEditRequest: CursoRequest
    {
        public int CursoId { get; set; }
    

    }
}
