using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Persistencia.DTOs.DataTables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services.Interfaces
{
    public interface ICursoService
    {
        Task<List<Curso>> ObterTodos();
        Task Cadastrar(CursoRequest curso);
        Task Deletar(int id);
        Task Desativar(int id);
        Task Ativar(int id);
        Task<ColecaoPaginada<Curso>> ObterListaPaginada(DataTableRequest request);
        Task<Curso> Obter(int id);
        Task<Curso> ObterComCurriculos(int id);
        Task Atualizar(CursoEditRequest curso);
    }
}
