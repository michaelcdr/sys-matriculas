using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Repositorios.Interfaces
{
    public interface IDisciplinaRepositorio : IRepositorio<Disciplina>
    {
        Task<ColecaoPaginada<Disciplina>> ObterListaPaginada(DataTableRequest request);
        Task<List<Disciplina>> ObterTodasDoCurriculo(int id);
        Task<Disciplina> ObterComRequisitosEPreRequisitos(int id);
        
    }
}
