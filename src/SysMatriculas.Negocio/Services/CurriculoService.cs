using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Models;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.Exceptions;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Persistencia.Transacoes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services
{
    public class CurriculoService : ICurriculoService
    {
        private IUnitOfWork _uow;
        IUsuarioService _usuarioService;

        public CurriculoService(
            IUnitOfWork uow,
            IUsuarioService usuarioService
        )
        {
            _uow = uow;
            _usuarioService = usuarioService;
        }

        public async Task Atualizar(CurriculoRequest request)
        {
            if (!await _uow.Curriculos.NomeTaDisponivel(request.CurriculoId,request.Nome))
                throw new CurriculoJaCadastradoException("Já existe um currículo com o nome informado.");

            Curriculo curriculo = await _uow.Curriculos.Obter(request.CurriculoId);
            curriculo.Nome = request.Nome;            
            await _uow.CommitAsync();
        }

        public async Task Cadastrar(Curriculo curriculo)
        {
            if (!await _uow.Curriculos.NomeTaDisponivel(null, curriculo.Nome))
                throw new CurriculoJaCadastradoException("Já existe um currículo com o nome informado.");
            _uow.Curriculos.Cadastrar(curriculo);
            await _uow.CommitAsync();
        }

        public async Task Delete(int id)
        {
            //if (await _uow.Curriculos.TaEmUso(id))
            //{
            //    throw new CurriculoEmUsoException("Currículo em uso.");
            //}
            //else
            //{
                await _uow.Curriculos.Deletar(id);
                await _uow.CommitAsync();
            //}
        }

        public async Task<Curriculo> Obter(int id)
        {
            return await _uow.Curriculos.Obter(id);
        }

        public async Task<Curriculo> ObterComCurso(int id)
        {
            return await _uow.Curriculos.ObterComCurso(id);
        }

        public async Task<ColecaoPaginada<Curriculo>> ObterListaPaginada(DataTableRequest request)
        {
            ColecaoPaginada<Curriculo> curriculosPaginados = await _uow.Curriculos.ObterListaPaginada(request);
            return curriculosPaginados;
        }

        public async Task<List<Curriculo>> ObterTodos()
        {
            return await _uow.Curriculos.ObterTodos();
        }

        public async Task<List<Curriculo>> ObterTodosDoAluno(string login)
        {
            string usuarioId = (await _usuarioService.ObterUsuarioAtual(login)).Id;
            List<Curriculo> curriculos = await _uow.Curriculos.ObterTodosDoAluno(usuarioId);
            return curriculos;
        }

        public async Task<List<CurriculoComDesempenho>> ObterTodosDoAlunoComDesempenhos(string login)
        {
            var curriculosComDesempenhos = new List<CurriculoComDesempenho>();
            string usuarioId = (await _usuarioService.ObterUsuarioAtual(login)).Id;
            List<Curriculo> curriculos = await _uow.Curriculos.ObterTodosDoAluno(usuarioId);
            foreach (var item in curriculos.OrderBy(a => a.Curso.Nome))
            {
                var curriculo = new CurriculoComDesempenho(item.CurriculoId, item.Curso.Nome + " - " + item.Nome);
                foreach (var disciplina in item.Disciplinas.OrderBy(a => a.Semestre).ThenBy(e => e.Nome))
                {
                    curriculo.Disciplinas.Add(new DiscplinaComDesempenho
                    {
                        DisicplinaId = disciplina.CurriculoId,
                        Nome = disciplina.Nome,
                        Semestre = disciplina.Semestre,
                        Nota = disciplina.Desempenhos.Any(e => e.UsuarioId == usuarioId)
                            ? disciplina.Desempenhos.Single(e => e.UsuarioId == usuarioId).Nota
                            : null
                    });
                }
                curriculosComDesempenhos.Add(curriculo);
            }
            return curriculosComDesempenhos;
        }
    }
}
