namespace SysMatriculas.Dominio
{
    public class MatricularRequest
    {
        public MatricularRequest(string usuarioId, int curriculoId)
        {
            UsuarioId = usuarioId;
            CurriculoId = curriculoId;
        }

        public string UsuarioId { get; set; }
        public int CurriculoId { get; set; }
    }
}
