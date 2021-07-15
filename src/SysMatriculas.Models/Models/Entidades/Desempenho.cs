namespace SysMatriculas.Dominio
{
    public class Desempenho
    {
        public int UsuarioDesempenhoId { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public bool Aprovado { get; set; }
        public int? Nota { get; set; }
        public Disciplina Disciplina { get; set; }
        public int DisciplinaId { get; set; }
    }
}
