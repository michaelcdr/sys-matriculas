using Microsoft.EntityFrameworkCore;
using SysMatriculas.Persistencia.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Repositorios
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {        
        protected readonly DbContext _db;

        private readonly DbSet<TEntity> _dbSet;

        public Repositorio(DbContext context)
        {
            _db = context;
            _dbSet = _db.Set<TEntity>();
        }

        public async Task<TEntity> Obter(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Deletar(object id)
        {
            _dbSet.Remove(await Obter(id));
        }

        public void Cadastrar(TEntity entidade)
        {
            _dbSet.Add(entidade);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
