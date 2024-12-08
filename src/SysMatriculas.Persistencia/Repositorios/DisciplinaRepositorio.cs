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
    public class DisciplinaRepositorio : Repositorio<Disciplina>, IDisciplinaRepositorio
    {
        private AppDbContext _context;

        public DisciplinaRepositorio(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Cadastrar(Disciplina disciplina)
        {
            _context.Disciplinas.Add(disciplina);
        }

        public async Task<Disciplina> ObterComRequisitosEPreRequisitos(int id)
        {
            return await _context.Disciplinas
                .Include(e => e.CoRequisitos)
                .Include(e => e.PreRequisitos)
                .Include(e => e.Curriculo).SingleAsync(e => e.DisciplinaId == id);
        }

        public async Task<ColecaoPaginada<Disciplina>> ObterListaPaginada(DataTableRequestDisciplinas request)
        {
            IQueryable<Disciplina> disciplinaQuery = _context.Disciplinas.Include(e => e.Curriculo);

            //busca por nome...
            if (!string.IsNullOrEmpty(request.search.value))
                disciplinaQuery = disciplinaQuery.Where(p => p.Nome.Contains(request.search.value));

            if (request.CurriculoId.HasValue)
            {
                disciplinaQuery = disciplinaQuery.Where(d => d.CurriculoId == request.CurriculoId.Value);
            }

            //ordenação conforme coluna clicada...
            int colunaOrdenada = request.order[0].column;
            string sentidoOrdem = request.order[0].dir;
            IOrderedQueryable<Disciplina> disciplinaOrdered;

            switch (colunaOrdenada)
            {
                case 1:
                    if (sentidoOrdem == "asc")
                        disciplinaOrdered = disciplinaQuery.OrderBy(p => p.Nome);
                    else
                        disciplinaOrdered = disciplinaQuery.OrderByDescending(p => p.Nome);
                    break;

                default:
                    disciplinaOrdered = disciplinaQuery.OrderByDescending(p => p.Nome);
                    break;
            }

            //preparando query para retornar resultados paginados...
            var resultadosPaginados = await (from e in disciplinaOrdered
                                             select e).Skip(request.start)
                                                      .Take(request.length)
                                                      .ToArrayAsync();

            int totalDeDisciplina = disciplinaQuery.Count();
            int totalDeDisciplinaFiltradas = disciplinaOrdered.Count();

            return new ColecaoPaginada<Disciplina>(
                totalDeDisciplina,
                totalDeDisciplinaFiltradas,
                resultadosPaginados
            );
        }

        public async Task<List<Disciplina>> ObterTodasDoCurriculo(int id)
        {
            return await _context.Disciplinas
                .Include(a => a.PreRequisitos)
                .Include(a => a.CoRequisitos)
                .Where(e => e.CurriculoId == id)
                .OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<List<Disciplina>> ObterTodos()
        {
            return await _context.Disciplinas.OrderBy(c => c.Nome).ToListAsync();
        }
    }
}
