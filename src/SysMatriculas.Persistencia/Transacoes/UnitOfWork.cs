using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Repositorios;
using SysMatriculas.Persistencia.Repositorios.Interfaces;
using System;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Transacoes
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext _context;

        public ICursoRepositorio Cursos { get; private set; }
        public ICurriculoRepositorio Curriculos { get; private set; }
        public IDisciplinaRepositorio Disciplinas { get; private set; }
        public IUsuarioRepository Usuarios { get; private set; }

        public ICoRequisitosRepositorio CoRequisitos { get; private set; }
        public IPreRequisitosRepositorio PreRequisitos { get; private set; }
        public IUsuarioDesempenhoRepositorio Desempenhos { get; private set; }
        public IMatriculaRepositorio Matriculas { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Cursos = new CursoRepositorio(context);
            Curriculos = new CurriculoRepositorio(context);
            Disciplinas = new DisciplinaRepositorio(context);
            Usuarios = new UsuarioRepositorio(context);

            CoRequisitos = new CoRequisitosRepositorio(context);
            PreRequisitos = new PreRequisitosRepositorio(context);
            Desempenhos = new UsuarioDesempenhoRepositorio(context);
            Matriculas = new MatriculaRepositorio(context);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    _context.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
