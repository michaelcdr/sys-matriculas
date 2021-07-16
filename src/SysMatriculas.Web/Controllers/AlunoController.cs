using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Models;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.Exceptions;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Web.Extensions;
using SysMatriculas.Web.Helpers;
using SysMatriculas.Web.Models;
using SysMatriculas.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ICurriculoService _curriculoService;
        private readonly IAlunoService _alunoService;
        private readonly ISelectListItemHelper _selectListHelper;
        private readonly IDisciplinaService _disciplinaService;

        public AlunoController(
            IUsuarioService usuarioService,
            ICurriculoService curriculoService,
            IAlunoService alunoService,
            IDisciplinaService disciplinaService,
            ISelectListItemHelper selectListHelper)
        {
            _usuarioService = usuarioService;
            _curriculoService = curriculoService;
            _alunoService = alunoService;
            _selectListHelper = selectListHelper;
            _disciplinaService = disciplinaService;
        }

        [Authorize(Roles = "Coordenador")]
        public IActionResult Index() => View();

        [HttpPost, Authorize(Roles = "Coordenador")]
        public async Task<JsonResult> Listar(DataTableRequest request)
        {
            try
            {
                ColecaoPaginada<Usuario> cursosPaginados = await _usuarioService.ObterListaPaginada(request, "Aluno");

                return Json(new {
                    request.draw,
                    recordsTotal = cursosPaginados.RecordsTotal,
                    recordsFiltered = cursosPaginados.RecordsFiltered,
                    data = cursosPaginados.Itens
                });
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível obter os cursos", ex.Message));
            }
        }

        [HttpPost, Authorize(Roles = "Coordenador")]
        public async Task<JsonResult> Deletar(string id)
        {
            try
            {
                await _usuarioService.Remover(id);
                return Json(new SucessoResponse("Usuário excluído com sucesso."));
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível excluír.", ex.Message));
            }
        }

        [HttpPost, Authorize(Roles = "Coordenador")]
        public async Task<JsonResult> Ativar(string id)
        {
            try
            {
                await _usuarioService.Ativar(id);
                return Json(new SucessoResponse("Aluno ativado com sucesso."));
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível ativar o aluno.", ex.Message));
            }
        }

        [HttpPost, Authorize(Roles = "Coordenador")]
        public async Task<JsonResult> Desativar(string id)
        {
            try
            {
                await _usuarioService.Desativar(id);
                return Json(new SucessoResponse("Aluno desativado com sucesso."));
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível ativar o aluno.", ex.Message));
            }
        }

        [Authorize(Roles = "Coordenador")]
        public IActionResult Cadastrar() => View(new AlunoCadastroViewModel());

        [HttpPost, Authorize(Roles = "Coordenador")]
        public async Task<IActionResult> Cadastrar(AlunoCadastroViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioRequest request = model.ToUsuarioRequest();
                    request.TipoDeUsuario = "Aluno";
                    await _usuarioService.Cadastrar(request);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ErrorModel.Msg);
            }
            return View(model);
        }

        [Authorize(Roles = "Coordenador")]
        public async Task<IActionResult> Editar(string id)
        {
            Usuario usuario = await _usuarioService.Obter(id);
            AlunoEditViewModel viewModel = usuario.ToEditViewModel();
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Coordenador")]
        public async Task<IActionResult> Editar(AlunoEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioRequest request = model.ToUsuarioRequest();
                    request.UsuarioId = model.UsuarioId;
                    await _usuarioService.Atualizar(request);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ErrorModel.Msg);
            }
            return View(model);
        }

        [Authorize(Roles = "Coordenador")]
        public async Task<IActionResult> Matricular()
        {
            var viewModel = new AssociacaoAlunoComCurso
            {
                AlunosDisponiveis = await _selectListHelper.ObterAlunos(),
                CurriculosDisponiveis = await _selectListHelper.ObterCurriculosSelectList()
            };
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Coordenador")]
        public async Task<IActionResult> Matricular(AssociacaoAlunoComCurso model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = new MatricularRequest(model.UsuarioId, model.CurriculoId);
                    await _alunoService.Matricular(request);
                    return RedirectToAction("Index");
                }
            }
            catch (JaMatriculadoException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch 
            {
                ModelState.AddModelError("", ErrorModel.Msg);
            }
            model.AlunosDisponiveis = await _selectListHelper.ObterAlunos();
            model.CurriculosDisponiveis = await _selectListHelper.ObterCurriculosSelectList();
            return View(model);
        }

        [HttpPost, Authorize(Roles = "Aluno")]
        public async Task<JsonResult> AlterarDesempenho(ConcluirDisciplinaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string usuarioId = (await _usuarioService.ObterUsuarioAtual(User.Identity.Name)).Id;
                    var request = new ConcluirDisciplinaRequest(usuarioId, model.DisciplinaId, model.Nota, model.Nota >= 6);
                    await _alunoService.ConcluirDisciplina(request);
                    return Json(new SucessoResponse("Alteração efetuada com sucesso."));
                }
                else
                    return Json(new ErroResponse("Não foi possível atualizar a disciplina.",""));
            }
            catch (Exception ex)
            {
               return Json(new ErroResponse("Não foi possível atualizar a disciplina.", ex.Message));
            }
        }

        [Authorize(Roles = "Aluno")]
        public async Task<IActionResult> Curriculos()
        {
            List<Curriculo> curriculos = await _curriculoService.ObterTodosDoAluno(User.Identity.Name);
            ViewBag.UsuarioId = (await _usuarioService.ObterUsuarioAtual(User.Identity.Name)).Id;
            return View(curriculos);
        }

        [Authorize(Roles = "Aluno")]
        public async Task<IActionResult> Progresso(int id)
        {
            var curriculo = await _curriculoService.ObterComCurso(id);
            var model = new AlunoDesempenhoViewModel(id, curriculo.Curso.Nome);
            return View(model);
        }

        [Authorize(Roles = "Aluno")]
        public async Task<PartialViewResult> _Progresso(int id)
        {
            ViewBag.CurriculoId = id;
            List<SemestreComDisciplinas> semestres = await _disciplinaService.ObterDisciplinasSeparadasPorSemestre(id, User.Identity.Name);
            return PartialView(semestres);
        }      
        
        [Authorize(Roles = "Aluno")]
        public async Task<JsonResult> ObterConexoesDisciplinas(int curriculoId)
        {
            List<DisciplinaDetalhes> disciplinas = await _disciplinaService.ObterTodasComConexoes(curriculoId, User.Identity.Name);
            return Json(new { disciplinas });
        }

        [Authorize(Roles = "Aluno")]
        public async Task<ActionResult> Desempenho()
        {
            List<CurriculoComDesempenho> curriculos = await _curriculoService.ObterTodosDoAlunoComDesempenhos(User.Identity.Name);
            return View(curriculos);
        }

        [Authorize(Roles = "Aluno")]
        public async Task<ActionResult> MeuPerfil()
        {
            Usuario usuario = await _usuarioService.ObterUsuarioAtual(User.Identity.Name);
            return View(usuario);
        }
    }
}