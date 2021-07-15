using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Persistencia.Transacoes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services
{
    public class CursoService : ICursoService
    {
        private IUnitOfWork _uow;

        public CursoService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Cadastrar(CursoRequest request)
        {
            var curso = new Curso(request.Nome, string.Join(",", request.Turno))
            {
                Curriculos = new List<Curriculo>()
            };
            curso.Ativar();
            foreach (var nomeCurriculo in request.Curriculos)            
                curso.Curriculos.Add(new Curriculo(nomeCurriculo));

            _uow.Cursos.Cadastrar(curso);
            await _uow.CommitAsync();
        }

        public async Task Deletar(int id)
        {
            await _uow.Cursos.Deletar(id);
            await _uow.CommitAsync();
        }

        public async Task Desativar(int id)
        {
            Curso curso = await _uow.Cursos.Obter(id);
            curso.Desativar();
            await _uow.CommitAsync();
        }

        public async Task Ativar(int id)
        {
            Curso curso = await _uow.Cursos.Obter(id);
            curso.Ativar();
            await _uow.CommitAsync();
        }

        public async Task<Curso> Obter(int id)
        {
            return await _uow.Cursos.Obter(id);
        }

        public async Task<ColecaoPaginada<Curso>> ObterListaPaginada(DataTableRequest request)
        {
            ColecaoPaginada<Curso> cursosPaginados = await _uow.Cursos.ObterListaPaginada(request);
            return cursosPaginados;
        }

        public async Task<List<Curso>> ObterTodos()
        {
            return await _uow.Cursos.ObterTodos();
        }

        public async Task<Curso> ObterComCurriculos(int id)
        {
            return await _uow.Cursos.ObterComCurriculos(id);
        }

        public async Task Atualizar(CursoEditRequest request)
        {
            //atualizando dados básicos...
            Curso curso = await _uow.Cursos.ObterComCurriculos(request.CursoId);
            curso.Nome = request.Nome;
            curso.Turno = string.Join(",",request.Turno);
            
            List<string> curriculosAtuais = curso.Curriculos.Select(e => e.Nome).ToList();
            
            //adicionando curriculos novos
            foreach (var curriculoReq in request.Curriculos)
            {
                if (!curriculosAtuais.Any(curriculo => curriculoReq == curriculo))
                    curso.Curriculos.Add(new Curriculo(curriculoReq));
            }

            //removendo curriculos removidos pelo usuario
            var remover = curriculosAtuais.Except(request.Curriculos).ToList();
            foreach (var item in remover)
            {
                curso.Curriculos.Remove(curso.Curriculos.First(e => e.Nome == item));
            }

            await _uow.CommitAsync();            
        }

    }
}
