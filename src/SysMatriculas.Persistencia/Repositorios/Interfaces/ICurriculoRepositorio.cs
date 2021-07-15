using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Repositorios.Interfaces
{
    public interface ICurriculoRepositorio : IRepositorio<Curriculo>
    {
        Task<ColecaoPaginada<Curriculo>> ObterListaPaginada(DataTableRequest request);
        Task<bool> NomeTaDisponivel(int? curriculoId, string nome);
        Task<List<Curriculo>> ObterTodosDoAluno(string usuarioId);
        Task<Curriculo> ObterComCurso(int id);
    }
}
