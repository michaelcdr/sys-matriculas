namespace SysMatriculas.Dominio
{
    public class Matricula
    {
        public int MatriculaId { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}
