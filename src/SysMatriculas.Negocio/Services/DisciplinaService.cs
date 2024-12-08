using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.DTO;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Persistencia.DTOs.DataTables;
using SysMatriculas.Persistencia.Transacoes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services
{
    public class DisciplinaService : IDisciplinaService
    {
        private IUnitOfWork _uow;
        private IUsuarioService _usuarioService;

        public DisciplinaService(
            IUnitOfWork uow,
            IUsuarioService usuarioService)
        {
            _uow = uow;
            _usuarioService = usuarioService;
        }

        public async Task Atualizar(DisciplinaRequest request)
        {
            Disciplina disciplina = await _uow.Disciplinas.ObterComRequisitosEPreRequisitos((int)request.DisciplinaId);
            disciplina.CurriculoId = request.CurriculoId;
            disciplina.CargaHoraria = request.CargaHoraria;
            disciplina.Semestre = request.Semestre;

            //remove os atuais...
            if (disciplina.PreRequisitos!= null)
                foreach (var item in disciplina.PreRequisitos)
                    await _uow.PreRequisitos.Deletar(item.PreRequisitosId);

            if(disciplina.CoRequisitos != null)
                foreach (var item in disciplina.CoRequisitos)
                    await _uow.CoRequisitos.Deletar(item.CoRequisitosId);
            
            //adiciona os novos...
            if (request.PreRequisitos != null && request.PreRequisitos.Count() > 0)
                foreach (var disciplinaPreRequisitoId in request.PreRequisitos)
                    disciplina.PreRequisitos.Add(new PreRequisito(disciplinaPreRequisitoId));

            if (request.CoRequisitos != null && request.CoRequisitos.Count() > 0)
                foreach (var disciplinaId in request.CoRequisitos)
                    disciplina.CoRequisitos.Add(new CoRequisito(disciplinaId));

            await _uow.CommitAsync();
        }

        public async Task Cadastrar(DisciplinaRequest request)  
        {
            var disciplina = new Disciplina
            {
                CargaHoraria = request.CargaHoraria,
                Nome = request.Nome,
                Semestre = request.Semestre,
                CurriculoId = request.CurriculoId
            };
            _uow.Disciplinas.Cadastrar(disciplina);

            if (request.PreRequisitos != null && request.PreRequisitos.Count() > 0)
                foreach (var disciplinaPreRequisitoId in request.PreRequisitos)
                    disciplina.PreRequisitos.Add(new PreRequisito(disciplinaPreRequisitoId));

            if (request.CoRequisitos != null && request.CoRequisitos.Count() > 0)
                foreach (var disciplinaId in request.CoRequisitos)
                    disciplina.CoRequisitos.Add(new CoRequisito(disciplinaId));

            await _uow.CommitAsync();
        }

        public async Task Deletar(int id)
        {
            await _uow.Disciplinas.Deletar(id);
            await _uow.CommitAsync();
        }

        public async Task<Disciplina> Obter(int id)
        {
            return await _uow.Disciplinas.ObterComRequisitosEPreRequisitos(id);
        }

        public async Task<ColecaoPaginada<DisciplinaDTO>> ObterListaPaginada(DataTableRequestDisciplinas request)
        {
            var dadosPaginados = await _uow.Disciplinas.ObterListaPaginada(request);

            var curriculosPaginados = new ColecaoPaginada<DisciplinaDTO>(
                dadosPaginados.RecordsTotal,
                dadosPaginados.RecordsFiltered,
                dadosPaginados.Itens.Select(e => new DisciplinaDTO { 
                    DisciplinaId = e.DisciplinaId,
                    Nome = e.Nome,
                    Curriculo = e.Curriculo.Nome,
                    CurriculoId = e.CurriculoId,
                }).ToList()
            );
            return curriculosPaginados;
        }

        public async Task<List<Disciplina>> ObterTodas()
        {
            return await _uow.Disciplinas.ObterTodos();
        }

        public async Task<List<Disciplina>> ObterTodasDoCurriculo(int id)
        {
            return await _uow.Disciplinas.ObterTodasDoCurriculo(id);
        }

        public async Task<List<DisciplinaDetalhes>> ObterTodasComConexoes(int curriculoId, string login)
        {
            List<SemestreComDisciplinas> semestres = await ObterDisciplinasSeparadasPorSemestre(curriculoId, login);
            var disciplinas = new List<DisciplinaDetalhes>();
            foreach (var item in semestres)
            {
                if (item.Disciplinas.Any(a => a.CoRequisitosIds.Count() > 0) || item.Disciplinas.Any(a => a.PreRequisitosIds.Count() > 0))
                {
                    var disciplinasItem = item.Disciplinas.Where(a => a.CoRequisitosIds.Count() > 0 ||
                                                                      a.PreRequisitosIds.Count() > 0).ToList();

                    disciplinas.AddRange(disciplinasItem);
                }
            }

            return disciplinas;
        }

        public async Task<List<SemestreComDisciplinas>> ObterDisciplinasSeparadasPorSemestre(int curriculoId, string login)
        {
            var semestres = new List<SemestreComDisciplinas>();
            string usuarioId = (await _usuarioService.ObterUsuarioAtual(login)).Id;

            List<Disciplina> disciplinas = await _uow.Disciplinas.ObterTodasDoCurriculo(curriculoId);
            List<Desempenho> desempenho = await _uow.Desempenhos.ObterPorAlunoECurriculo(curriculoId, usuarioId);

            semestres = disciplinas
                            .Select(e => e.Semestre)
                            .Distinct()
                            .Select(e => new SemestreComDisciplinas
                            {
                                Nome = "Semestre " + e.ToString(),
                                Ordem = e,
                                Disciplinas = disciplinas.Where(a => a.CurriculoId == curriculoId && a.Semestre == e)
                                    .OrderBy(d => d.Ordem)
                                    .ToList()
                                    .Select(d => new DisciplinaDetalhes {
                                        DisciplinaId = d.DisciplinaId,
                                        Nome = d.Nome,
                                        Ordem = d.Ordem,
                                        Nota = desempenho.Any(a => a.DisciplinaId == d.DisciplinaId)
                                            ? desempenho.Single(a => a.DisciplinaId == d.DisciplinaId).Nota
                                            : null,
                                        Aprovado = desempenho.Any(a => a.DisciplinaId == d.DisciplinaId && a.Nota >= 6),
                                        TemPreRequisitoNaoConcluido = VerificarPreRequisitos(d, desempenho),
                                        TemConexao = d.CoRequisitos.Any() || d.PreRequisitos.Any(),
                                        CoRequisitosIds = d.CoRequisitos != null
                                            ? d.CoRequisitos.Select(co => co.DisciplinaId).ToList()
                                            : new List<int>(),
                                        PreRequisitosIds = d.PreRequisitos != null
                                            ? d.PreRequisitos.Select(pre => pre.DisciplinaId).ToList()
                                            : new List<int>(),

                                    }).OrderBy(d1 => d1.Ordem).ToList()

                        }).OrderBy(e => e.Ordem).ToList();

            
            return semestres;
        }

        private bool VerificarPreRequisitos(Disciplina disciplina, List<Desempenho> desempenhos)
        {
            if (disciplina.PreRequisitos == null || disciplina.PreRequisitos.Count() == 0)
                return false;
            else
            {
                int qtdPreRequisitoNomConcluido = 0;
                foreach (var item in disciplina.PreRequisitos)
                    if (!desempenhos.Any(e => e.Nota >= 6 && e.DisciplinaId == item.DisciplinaId))
                        qtdPreRequisitoNomConcluido++;
                
                return qtdPreRequisitoNomConcluido > 0;
            }
        }

        public async Task<bool> TemPreRequisitosNaoConcluidos(int disciplinaId, int curriculoId, string usuarioId)
        {
            var disciplina = await _uow.Disciplinas.ObterComRequisitosEPreRequisitos(disciplinaId);
            var idsPreRequisitos = new List<int>();
            if (disciplina.PreRequisitos == null || disciplina.PreRequisitos.Count() == 0)
                return false;
            else
            {
                idsPreRequisitos = disciplina.PreRequisitos.Select(e => e.DisciplinaId).ToList();
                //ja sabemos que tem prerequisitos, entao agora veremos se estao concluidos...
                var request = new DesempenhoDisciplinasAlunoRequest(idsPreRequisitos, usuarioId);
                List<Desempenho> desempenhosDosPreRequisitos = await _uow.Desempenhos.ObterPorAlunoEDisciplinas(request);
                int qtdReprovacao = 0;
                foreach (var item in idsPreRequisitos)
                {
                    if (!desempenhosDosPreRequisitos.Any(e => e.Nota >= 6 && e.DisciplinaId == item))
                    {
                        qtdReprovacao++;
                    }
                }
                return qtdReprovacao > 0; //se true tem prerequisitos nao concluidos
            }
        }
    }
}
