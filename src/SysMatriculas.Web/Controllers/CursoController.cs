using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Web.Extensions;
using SysMatriculas.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Web.Controllers
{
    [Authorize(Roles = "Coordenador")]
    public class CursoController : Controller
    {
        private ICursoService _cursoService;
        private ICurriculoService _curriculoService;

        public CursoController(
            ICursoService cursoService,
            ICurriculoService curriculoService)
        {
            _cursoService = cursoService;
            _curriculoService = curriculoService;
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
                var cursosPaginados = await _cursoService.ObterListaPaginada(request);
                return Json(new
                {
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

        public IActionResult Cadastrar()
        {
            return View(new CursoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CursoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curso = new CursoRequest(model.Nome, model.Turno, model.Curriculos);
                    await _cursoService.Cadastrar(curso);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }            
            return View(model);
        }

        public async Task<IActionResult> Editar(int id)
        {
            Curso curso = await _cursoService.ObterComCurriculos(id);
            var curriculoAtuais = new List<CurriculoResponse>();
            foreach (var item in curso.Curriculos)
                curriculoAtuais.Add(new CurriculoResponse(item.CurriculoId, item.Nome));

            CursoEditViewModel viewModel = curso.ToCursoEditViewModel();
            List<Curriculo> curriculos = await _curriculoService.ObterTodos();
            viewModel.CurriculosPossiveis = curriculos.Select(c => c.ToCurriculoResponse())
                                                      .ToList();
            viewModel.CurriculosAtuais = curriculoAtuais;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, CursoEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CursoEditRequest request = model.ToCursoEditRequest();
                    await _cursoService.Atualizar(request);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Deletar(int id)
        {
            try
            {
                await _cursoService.Deletar(id);
                return Json(new SucessoResponse("Removido com sucesso."));
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível remover o curso", ex.Message));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Desativar(int id)
        {
            try
            {
                await _cursoService.Desativar(id);
                return Json(new SucessoResponse("Curso desativado com sucesso"));
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível desativar o curso", ex.Message));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Ativar(int id)
        {
            try
            {
                await _cursoService.Ativar(id);
                return Json(new SucessoResponse("Curso ativado com sucesso"));
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível ativar o curso", ex.Message));
            }
        }
    }
}