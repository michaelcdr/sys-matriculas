using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Repositorios.Interfaces;

namespace SysMatriculas.Persistencia.Repositorios
{
    public class UsuarioDesempenhoRepositorio 
        : Repositorio<Desempenho>, IUsuarioDesempenhoRepositorio
    {
        private AppDbContext _context;

        public UsuarioDesempenhoRepositorio(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Concluir(int usuarioDesempenhoId, ConcluirDisciplinaRequest request)
        {
            var desempenho = await Obter(usuarioDesempenhoId);
            desempenho.Nota = request.Nota;
            desempenho.UsuarioId = request.UsuarioId;
            desempenho.Nota = request.Nota;
            desempenho.DisciplinaId = request.DisciplinaId;
            desempenho.Aprovado = request.Aprovado;
        }

        public async Task<List<Desempenho>> ObterPorAlunoECurriculo(int curriculoId, string usuarioId)
        {
            return await (from d in _context.Desempenhos
                          where d.Disciplina.CurriculoId == curriculoId &&
                                d.UsuarioId == usuarioId
                          select d).Include(e => e.Disciplina).ThenInclude(a => a.Curriculo)
                                   .Include(e => e.Usuario)
                                   .ToListAsync();
        }

        public Task<List<Desempenho>> ObterPorAlunoECurriculos(string usuarioId, List<int> curriculosIds)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Desempenho> ObterPorAlunoEDisciplina(DesempenhoAlunoRequest request)
        {
            return await _context.Desempenhos.SingleOrDefaultAsync(s => s.DisciplinaId == request.DisciplinaId && s.UsuarioId == request.UsuarioId);
        }

        public async Task<List<Desempenho>> ObterPorAlunoEDisciplinas(DesempenhoDisciplinasAlunoRequest request)
        {
            return await (from d in _context.Desempenhos
                          where request.DisciplinasIds.Contains(d.DisciplinaId) && 
                                d.UsuarioId == request.UsuarioId
                          select d).ToListAsync();
        }
    }
}
