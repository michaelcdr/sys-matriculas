using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Repositorios.Interfaces
{
    public interface ICursoRepositorio : IRepositorio<Curso>
    {
        Task<ColecaoPaginada<Curso>> ObterListaPaginada(DataTableRequest request);
        Task<Curso> ObterComCurriculos(int id);
    }
}
