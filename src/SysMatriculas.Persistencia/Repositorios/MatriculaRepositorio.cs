using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysMatriculas.Dominio;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Repositorios.Interfaces;

namespace SysMatriculas.Persistencia.Repositorios
{
    public class MatriculaRepositorio : Repositorio<Matricula>, IMatriculaRepositorio
    {
        private AppDbContext _context;

        public MatriculaRepositorio(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Matricular(MatricularRequest request)
        {
            await _context.Matriculas.AddAsync(new Matricula {
                CurriculoId = request.CurriculoId,
                UsuarioId = request.UsuarioId
            });
        }

        public async Task<Matricula> ObterPorAlunoECurriculo(MatricularRequest request)
        {
            return await _context.Matriculas.SingleOrDefaultAsync(e => e.CurriculoId == request.CurriculoId &&
                                                                       e.UsuarioId == request.UsuarioId);
        }
    }
}
