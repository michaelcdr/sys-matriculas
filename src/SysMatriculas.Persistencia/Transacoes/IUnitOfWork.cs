using SysMatriculas.Persistencia.Repositorios.Interfaces;
using System;
using System.Threading.Tasks;

namespace SysMatriculas.Persistencia.Transacoes
{
    public interface IUnitOfWork : IDisposable
    {
        ICurriculoRepositorio Curriculos { get; }
        ICursoRepositorio Cursos { get; }
        IDisciplinaRepositorio Disciplinas { get; }
        IUsuarioRepository Usuarios { get;  }

        IPreRequisitosRepositorio PreRequisitos { get; }
        ICoRequisitosRepositorio CoRequisitos { get; }
        IUsuarioDesempenhoRepositorio Desempenhos { get; }
        IMatriculaRepositorio Matriculas { get; }

        int Commit();
        Task<int> CommitAsync();
    }
}
