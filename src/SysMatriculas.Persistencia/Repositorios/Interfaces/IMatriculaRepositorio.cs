using System.Threading.Tasks;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.Repositorios.Interfaces
{
    public interface IMatriculaRepositorio : IRepositorio<Matricula>
    {
        Task Matricular(MatricularRequest request);
        Task<Matricula> ObterPorAlunoECurriculo(MatricularRequest request);
    }
}
