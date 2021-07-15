using Microsoft.EntityFrameworkCore;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Repositorios
{
    public class CursoRepositorio : Repositorio<Curso>, ICursoRepositorio
    {
        private AppDbContext _context;

        public CursoRepositorio(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Cadastrar(Curso curso)
        {
            _context.Cursos.Add(curso);
        }

        public async Task<Curso> ObterComCurriculos(int id)
        {
            //return await _context.Cursos
            //                    .Include(e => e.CursosCurriculos)
            //                    .ThenInclude(e => e.Curriculo)    
            //                    .SingleOrDefaultAsync(c => c.CursoId == id);
            return await _context.Cursos.Include(e => e.Curriculos)
                                .SingleOrDefaultAsync(c => c.CursoId == id);
        }

        public async Task<ColecaoPaginada<Curso>> ObterListaPaginada(DataTableRequest request)
        {
            IQueryable<Curso> cursosQuery = _context.Cursos;

            //busca por nome ou turno...
            if (!string.IsNullOrEmpty(request.search.value))
                cursosQuery = cursosQuery.Where(p => p.Nome.Contains(request.search.value) ||
                                                     p.Turno.Contains(request.search.value));

            //ordenação conforme coluna clicada...
            int colunaOrdenada = request.order[0].column;
            string sentidoOrdem = request.order[0].dir;
            IOrderedQueryable<Curso> cursosOrdered;

            switch (colunaOrdenada)
            {
                case 1:
                    if (sentidoOrdem == "asc")
                        cursosOrdered = cursosQuery.OrderBy(p => p.Nome);
                    else
                        cursosOrdered = cursosQuery.OrderByDescending(p => p.Nome);
                    break;

                case 2:
                    if (sentidoOrdem == "asc")
                        cursosOrdered = cursosQuery.OrderBy(p => p.Turno);
                    else
                        cursosOrdered = cursosQuery.OrderByDescending(p => p.Turno);
                    break;

                default:
                    cursosOrdered = cursosQuery.OrderByDescending(p => p.Nome);
                    break;
            }

            //preparando query para retornar resultados paginados...
            var resultadosPaginados = await (from e in cursosOrdered
                                             select e).Skip(request.start)
                                                      .Take(request.length)
                                                      .ToArrayAsync();

            int totalDeCursos = cursosQuery.Count();
            int totalDeCursosFiltrados = cursosOrdered.Count();

            return new ColecaoPaginada<Curso>(
                totalDeCursos, 
                totalDeCursosFiltrados, 
                resultadosPaginados
            );
        }

        public async Task<List<Curso>> ObterTodos()
        {
            return await _context.Cursos.OrderBy(e => e.Nome).ToListAsync();
        }
    }
}
