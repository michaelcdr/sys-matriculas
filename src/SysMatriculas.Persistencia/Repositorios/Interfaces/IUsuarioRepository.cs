using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Repositorios.Interfaces
{
    public interface IUsuarioRepository : IRepositorio<Usuario>
    {
        Task<ColecaoPaginada<Usuario>> ObterListaPaginada(DataTableRequest request, string tipo);
        Task<Usuario> ObterPeloLogin(string login);
        Task Ativar(object id);
        Task Desativar(object id);
        Task<List<Usuario>> ObterTodosAlunos();
    }
}
