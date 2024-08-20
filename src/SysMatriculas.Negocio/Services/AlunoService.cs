using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Negocio.Exceptions;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Persistencia.Transacoes;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services
{
    public class AlunoService : IAlunoService
    {
        private IUnitOfWork _uow;

        public AlunoService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ConcluirDisciplina(ConcluirDisciplinaRequest request)
        {   
            Desempenho desempenho = await _uow.Desempenhos.ObterPorAlunoEDisciplina(
                new DesempenhoAlunoRequest(request.UsuarioId, request.DisciplinaId)
            );

            if (desempenho == null)
            {
                _uow.Desempenhos.Cadastrar(new Desempenho
                {
                    Aprovado = request.Nota >= 6,
                    UsuarioId = request.UsuarioId,
                    DisciplinaId = request.DisciplinaId,
                    Nota = request.Nota
                });
            }
            else
                await _uow.Desempenhos.Concluir(desempenho.UsuarioDesempenhoId, request);

            await _uow.CommitAsync();
        }

        public async Task Matricular(MatricularRequest request)
        {
            Matricula matricula = await _uow.Matriculas.ObterPorAlunoECurriculo(request);
            if (matricula == null)
            {
                await _uow.Matriculas.Matricular(request);
                await _uow.CommitAsync();
            }
            else
                throw new JaMatriculadoException();
        }
    }
}
