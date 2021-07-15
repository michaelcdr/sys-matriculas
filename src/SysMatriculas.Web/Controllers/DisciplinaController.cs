using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Web.Extensions;
using SysMatriculas.Web.Helpers;
using SysMatriculas.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Web.Controllers
{
    [Authorize(Roles = "Coordenador")]
    public class DisciplinaController : Controller
    {
        private IDisciplinaService _disciplinaService;
        private ICurriculoService _curriculoService;
        private ICursoService _cursoService;
        private ISelectListItemHelper _selectListHelper;

        public DisciplinaController(
            IDisciplinaService disciplinaService,
            ICurriculoService curriculoService,
            ICursoService cursoService,
            ISelectListItemHelper selectListHelper)
        {
            _disciplinaService = disciplinaService;
            _curriculoService = curriculoService;
            _cursoService = cursoService;
            _selectListHelper = selectListHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Listar(DataTableRequest request)
        {
            try
            {
                var disciplinasPaginados = await _disciplinaService.ObterListaPaginada(request);
                return Json(new
                {
                    request.draw,
                    recordsTotal = disciplinasPaginados.RecordsTotal,
                    recordsFiltered = disciplinasPaginados.RecordsFiltered,
                    data = disciplinasPaginados.Itens
                });
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível obter os estabelecimentos", ex.Message));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Deletar(int id)
        {
            try
            {
                await _disciplinaService.Deletar(id);
                return Json(new SucessoResponse("Disciplina removida com sucesso."));
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível remover as disciplinas.", ex.Message));
            }
        }

        public async Task<IActionResult> Cadastrar()
        {
            List<SelectListItem> selectListItems = await _selectListHelper.ObterCurriculosSelectList();
            var viewModel = new DisciplinaCadastroViewModel(selectListItems);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(DisciplinaCadastroViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DisciplinaRequest disciplina = model.ToDisciplinaRequest();
                    await _disciplinaService.Cadastrar(disciplina);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível cadastrar a disciplina");
            }
            model.CurriculosDisponiveis = await _selectListHelper.ObterCurriculosSelectList();
            return View(model);
        }

        public async Task<IActionResult> Editar(int id)
        {
            Disciplina disciplina = await _disciplinaService.Obter(id);
            DisciplinaEdicaoViewModel model = disciplina.ToDisciplinaEdicaoViewModel();
            model.CurriculosDisponiveis = await _selectListHelper.ObterCurriculosSelectList();
            model.PreRequisitosDisponiveis = await _selectListHelper.ObterSelectListDisciplinas(disciplina.CurriculoId,disciplina.DisciplinaId);
            model.CoRequisitosDisponiveis = model.PreRequisitosDisponiveis;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(DisciplinaEdicaoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DisciplinaRequest disciplina = model.ToDisciplinaRequest();
                    await _disciplinaService.Atualizar(disciplina);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível atualizar a disciplina.");
            }

            List<SelectListItem> disciplinasSelectList = await _selectListHelper.ObterSelectListDisciplinas((int)model.CurriculoId, model.DisciplinaId);
            model.PreRequisitosDisponiveis = disciplinasSelectList;
            model.CoRequisitosDisponiveis = disciplinasSelectList;
            model.CurriculosDisponiveis = await _selectListHelper.ObterCurriculosSelectList();
            return View(model);
        }


        public async Task<JsonResult> ObterDisciplinasDoCurriculo(int id)
        {
            try
            {
                List<Disciplina> disciplinasItens = await _disciplinaService.ObterTodasDoCurriculo(id);
                List<DisciplinaResponse> disciplinas = disciplinasItens.Select(item => item.ToDisciplinaResponse()).ToList();
                return Json(new { sucesso = true, disciplinas });
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível obter uma lista de disciplinas.", ex.Message));
            }
        }

        
    }
}