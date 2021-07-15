using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Repositorios.Interfaces
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        Task Deletar(object id);
        Task<TEntity> Obter(object id);
        void Cadastrar(TEntity entidade);
        Task<List<TEntity>> ObterTodos();
    }
}
