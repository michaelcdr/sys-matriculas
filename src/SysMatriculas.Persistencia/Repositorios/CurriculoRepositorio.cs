using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Persistencia.DTOs.DataTables;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Repositorios.Interfaces;

namespace SysMatriculas.Persistencia.Repositorios
{
    public class CurriculoRepositorio : Repositorio<Curriculo>, ICurriculoRepositorio
    {
        private AppDbContext _context;

        public CurriculoRepositorio(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Cadastrar(Curriculo curriculo)
        {
            _context.Curriculos.Add(curriculo);
        }

        public async Task<bool> NomeTaDisponivel(int? curriculoId, string nome)
        {
            if (curriculoId == null)
                return !await _context.Curriculos.AnyAsync(e => e.Nome == nome);
            else
            {
                return !await _context.Curriculos.AnyAsync(e => e.Nome == nome && e.CurriculoId != curriculoId);
            }
        }

        public async Task<Curriculo> ObterComCurso(int id)
        {
            return await _context.Curriculos
                .Include(e => e.Curso)
                .SingleAsync(e => e.CurriculoId == id);
                
        }

        public async Task<ColecaoPaginada<Curriculo>> ObterListaPaginada(DataTableRequest request)
        {
            IQueryable<Curriculo> curriculosQuery = _context.Curriculos;

            //busca por nome...
            if (!string.IsNullOrEmpty(request.search.value))
                curriculosQuery = curriculosQuery.Where(p => p.Nome.Contains(request.search.value));

            //ordenação conforme coluna clicada...
            int colunaOrdenada = request.order[0].column;
            string sentidoOrdem = request.order[0].dir;
            IOrderedQueryable<Curriculo> curriculosOrdered;

            switch (colunaOrdenada)
            {
                case 1:
                    if (sentidoOrdem == "asc")
                        curriculosOrdered = curriculosQuery.OrderBy(p => p.Nome);
                    else
                        curriculosOrdered = curriculosQuery.OrderByDescending(p => p.Nome);
                    break;

                default:
                    curriculosOrdered = curriculosQuery.OrderBy(p => p.Nome);
                    break;
            }

            //preparando query para retornar resultados paginados...
            var resultadosPaginados = await (from e in curriculosOrdered
                                             select e).Skip(request.start)
                                                      .Take(request.length)
                                                      .ToArrayAsync();

            int totalDeCursos = curriculosQuery.Count();
            int totalDeCursosFiltrados = curriculosOrdered.Count();

            return new ColecaoPaginada<Curriculo>(
                totalDeCursos,
                totalDeCursosFiltrados,
                resultadosPaginados
            );
        }

        public async Task<List<Curriculo>> ObterTodos()
        {
            return await _context.Curriculos.Include(a => a.Curso)
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
        }

        public async Task<List<Curriculo>> ObterTodosDoAluno(string usuarioId)
        {
            List<Curriculo> curriculos = await (from c in _context.Curriculos
                                                where c.Matriculas.Any(m => m.UsuarioId == usuarioId)
                                                select c
                                                ).Include(c => c.Disciplinas)
                                                    .ThenInclude(e => e.Desempenhos)
                                                 .Include(c => c.Curso)
                                                 .OrderBy(c => c.Nome)
                                                 .ToListAsync();
            return curriculos;
        }
    }
}
