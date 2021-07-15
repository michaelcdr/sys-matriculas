using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Models;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services.Interfaces
{
    public interface ICurriculoService
    {
        Task<List<Curriculo>> ObterTodos();
        //Task<List<CurriculoComCurso>> ObterTodosComCurso();
        Task<Curriculo> Obter(int id);
        Task Cadastrar(Curriculo curso);
        Task<ColecaoPaginada<Curriculo>> ObterListaPaginada(DataTableRequest request);
        Task Atualizar(CurriculoRequest request);
        Task Delete(int id);
        Task<List<Curriculo>> ObterTodosDoAluno(string name);
        Task<List<CurriculoComDesempenho>> ObterTodosDoAlunoComDesempenhos(string name);
        Task<Curriculo> ObterComCurso(int id);
    }
}
