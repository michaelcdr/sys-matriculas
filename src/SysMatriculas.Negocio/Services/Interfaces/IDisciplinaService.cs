using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.DTO;
using SysMatriculas.Persistencia.DTOs.DataTables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services.Interfaces
{
    public interface IDisciplinaService
    {
        Task Cadastrar(DisciplinaRequest curso);
        Task<Disciplina> Obter(int id);
        Task<ColecaoPaginada<DisciplinaDTO>> ObterListaPaginada(DataTableRequestDisciplinas request);
        Task Deletar(int id);

        Task<List<Disciplina>> ObterTodas();
        Task<List<Disciplina>> ObterTodasDoCurriculo(int id);
        Task Atualizar(DisciplinaRequest disciplina);
        Task<List<SemestreComDisciplinas>> ObterDisciplinasSeparadasPorSemestre(int curriculoId, string login);
        Task<List<DisciplinaDetalhes>> ObterTodasComConexoes(int curriculoId, string login);
        Task<bool> TemPreRequisitosNaoConcluidos(int disciplinaId, int curriculoId, string usuarioId);
    }
}
