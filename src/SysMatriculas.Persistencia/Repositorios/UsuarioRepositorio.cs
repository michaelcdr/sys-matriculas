using Microsoft.EntityFrameworkCore;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Persistencia.DTOs.DataTables;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepository
    {
        private AppDbContext _context;

        public UsuarioRepositorio(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObterTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task Ativar(object id)
        {
            Usuario usuario = await Obter(id);
            usuario.Ativo = true;
        }

        public async Task Desativar(object id)
        {
            Usuario usuario = await Obter(id);
            usuario.Ativo = false;
        }

        public void Cadastrar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public async Task<ColecaoPaginada<Usuario>> ObterListaPaginada(DataTableRequest request, string tipo = "Aluno")
        {
            IQueryable<Usuario> usuariosQuery = (from u in _context.Usuarios
                                                 join ur in _context.UserRoles on u.Id equals ur.UserId
                                                 join r in _context.Roles on ur.RoleId equals r.Id
                                                 where r.Name == tipo
                                                 select u);

            //busca por nome...
            if (!string.IsNullOrEmpty(request.search.value))
                usuariosQuery = usuariosQuery.Where(p => p.UserName.Contains(request.search.value));

            //ordenação conforme coluna clicada...
            int colunaOrdenada = request.order[0].column;
            string sentidoOrdem = request.order[0].dir;
            IOrderedQueryable<Usuario> usuariosOrdered;

            switch (colunaOrdenada)
            {
                case 1:
                    if (sentidoOrdem == "asc")
                        usuariosOrdered = usuariosQuery.OrderBy(p => p.Nome);
                    else
                        usuariosOrdered = usuariosQuery.OrderByDescending(p => p.Nome);
                    break;

                default:
                    usuariosOrdered = usuariosQuery.OrderByDescending(p => p.Nome);
                    break;
            }

            //preparando query para retornar resultados paginados...
            var resultadosPaginados = await (from e in usuariosOrdered
                                             select e).Skip(request.start)
                                                      .Take(request.length)
                                                      .ToArrayAsync();

            int totalDeUsuarios = usuariosQuery.Count();
            int totalDeUsuariosFiltrados = usuariosOrdered.Count();

            return new ColecaoPaginada<Usuario>(
                totalDeUsuarios,
                totalDeUsuariosFiltrados,
                resultadosPaginados
            );
        }

        public async Task<Usuario> ObterPeloLogin(string login)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(e => e.UserName == login);
        }

        public async Task<List<Usuario>> ObterTodosAlunos()
        {
            return await (from u in _context.Usuarios
                          join ur in _context.UserRoles on u.Id equals ur.UserId
                          join r in _context.Roles on ur.RoleId equals r.Id
                          where r.Name == "Aluno"
                          select u).OrderBy(e => e.Nome).ToListAsync();
        }
    }
}
