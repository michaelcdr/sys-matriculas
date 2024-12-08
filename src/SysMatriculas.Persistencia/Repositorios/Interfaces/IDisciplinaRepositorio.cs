using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Persistencia.DTOs.DataTables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Repositorios.Interfaces
{
    public interface IDisciplinaRepositorio : IRepositorio<Disciplina>
    {
        Task<ColecaoPaginada<Disciplina>> ObterListaPaginada(DataTableRequestDisciplinas request);
        Task<List<Disciplina>> ObterTodasDoCurriculo(int id);
        Task<Disciplina> ObterComRequisitosEPreRequisitos(int id);
        
    }
}
