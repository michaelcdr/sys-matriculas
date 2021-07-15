using System.Collections.Generic;
using System.Threading.Tasks;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;

namespace SysMatriculas.Persistencia.Repositorios.Interfaces
{
    public interface IUsuarioDesempenhoRepositorio : IRepositorio<Desempenho>
    {
        Task Concluir(int usuarioDesempenhoId, ConcluirDisciplinaRequest request);
        Task<Desempenho> ObterPorAlunoEDisciplina(DesempenhoAlunoRequest request);
        Task<List<Desempenho>> ObterPorAlunoEDisciplinas(DesempenhoDisciplinasAlunoRequest request);
        Task<List<Desempenho>> ObterPorAlunoECurriculo(int curriculoId, string usuarioId);
        Task<List<Desempenho>> ObterPorAlunoECurriculos(string usuarioId, List<int> curriculosIds);
    }
}
